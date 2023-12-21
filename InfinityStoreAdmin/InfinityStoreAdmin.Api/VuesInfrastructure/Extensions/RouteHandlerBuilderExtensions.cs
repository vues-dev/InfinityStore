using InfinityStoreAdmin.Api.VuesInfrastructure.Filters;

namespace InfinityStoreAdmin.Api.VuesInfrastructure.Extensions;

public static class RouteHandlerBuilderExtensions
{
    public static RouteHandlerBuilder ValidateRequest(this RouteHandlerBuilder handlerBuilder)
    {
        return handlerBuilder.AddEndpointFilter<ValidationFilter>();
    }
}

