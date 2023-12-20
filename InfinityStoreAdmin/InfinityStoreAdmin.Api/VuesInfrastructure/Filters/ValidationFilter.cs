using System;
using FluentValidation;
using FluentValidation.Results;
using Vues.Net.Models;

namespace Vues.Net.Filters;

public class ValidationFilter : IEndpointFilter
{
    public async ValueTask<object?> InvokeAsync(EndpointFilterInvocationContext context, EndpointFilterDelegate next)
    {
        dynamic validator = null;
        dynamic argToValidate = null;

        foreach (var item in context.Arguments)
        {
            // Dynamically resolve the validator type based on the type of the argument.
            var argType = item.GetType();
            var validatorType = typeof(IValidator<>).MakeGenericType(argType);
            validator = context.HttpContext.RequestServices.GetService(validatorType);

            if (validator != null)
            {
                argToValidate = item;
                break;
            }
        }

        if (validator == null)
        {
            return new InvalidOperationException("No validator found for http context. Check handler arguments to have validator applied");
        }

        ValidationResult validationResult = await validator.ValidateAsync(argToValidate);

        if (!validationResult.IsValid)
        {
            var errors = validationResult.Errors
                .Select(e => new { e.PropertyName, e.ErrorMessage })
                .ToArray();
            return Results.UnprocessableEntity(new { Errors = errors });
        }

        return await next.Invoke(context);
    }
}
