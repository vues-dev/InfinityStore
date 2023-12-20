using System;
using FluentValidation;
using Vues.Net.Models;

namespace Vues.Net.Filters;

public class ValidationFilter<T> : IEndpointFilter where T : class
{
    public async ValueTask<object?> InvokeAsync(EndpointFilterInvocationContext context, EndpointFilterDelegate next)
    {
        T? argToValidate = context.Arguments.First(x => x?.GetType() == typeof(T)) as T;
        IValidator<T>? validator = context.HttpContext.RequestServices.GetService<IValidator<T>>();

        if (validator is not null)
        {
            var validationResult = await validator.ValidateAsync(argToValidate!);
            if (!validationResult.IsValid)
            {
                var res = new ValidationError()
                {
                    Errors = validationResult.ToDictionary()
                };
                return Results.UnprocessableEntity(res);
            }
        }

        return await next.Invoke(context);
    }
}