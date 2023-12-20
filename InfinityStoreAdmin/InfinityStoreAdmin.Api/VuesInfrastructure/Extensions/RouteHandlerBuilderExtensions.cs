using System;
using Vues.Net.Filters;

namespace Vues.Net;

public static class RouteHandlerBuilderExtensions
{
    public static RouteHandlerBuilder ValidateRequest<T>(this RouteHandlerBuilder handlerBuilder) where T : class
    {
        return handlerBuilder.AddEndpointFilter<ValidationFilter<T>>();
    }
}

