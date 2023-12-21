using Microsoft.OpenApi.Models;

namespace InfinityStoreAdmin.Api.Shared.Configurations
{
    public static class SwaggerConfig
    {
        internal static List<OpenApiTag> GAMES_TAG = new() { new() { Name = "Games" } };
    }
}

