using System;
using FluentValidation;
using FluentValidation.Results;
using Vues.Net.Models;

namespace Vues.Net.Filters;

public class ValidationFilter : IEndpointFilter
{
    public async ValueTask<object?> InvokeAsync(EndpointFilterInvocationContext context, EndpointFilterDelegate next)
    {
        IValidator? validator = null;
        object? argToValidate = null;

        foreach (var item in context.Arguments)
        {
            if(item is null)
                continue;
                
            var validatorType = typeof(IValidator<>).MakeGenericType(item!.GetType());
            validator = context.HttpContext.RequestServices.GetService(validatorType) as IValidator;

            if (validator != null)
            {
                argToValidate = item;
                break;
            }
        }

        if (validator is null)
        {
            return new InvalidOperationException("No validator found for http context. Check handler arguments to have validator applied");
        }

        var validationContext = new ValidationContext<object>(argToValidate!);

        ValidationResult validationResult = await validator.ValidateAsync(validationContext);

        if (!validationResult.IsValid)
        {
            var res = new ValidationError()
            {
                Errors = validationResult.ToDictionary()
            };
            return Results.UnprocessableEntity(res);
        }

        return await next.Invoke(context);
    }
}
