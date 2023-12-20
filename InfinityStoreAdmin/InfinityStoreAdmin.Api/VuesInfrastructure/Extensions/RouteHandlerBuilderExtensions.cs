using System;
using Vues.Net.Filters;

namespace Vues.Net;

public static class RouteHandlerBuilderExtensions
{
    public static RouteHandlerBuilder ValidateRequest(this RouteHandlerBuilder handlerBuilder)
    {
        return handlerBuilder.AddEndpointFilter<ValidationFilter>();
    }
}

