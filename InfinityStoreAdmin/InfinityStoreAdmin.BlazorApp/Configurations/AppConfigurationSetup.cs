

using InfinityStoreAdmin.Api.Shared.Configurations;
using InfinityStoreAdmin.BlazorApp.Services.Interfaces;
using Refit;

namespace InfinityStoreAdmin.BlazorApp.Configurations
{
    public class AppConfigurationSetup : IConfigurationSetup
    {
        public void ConfigureAll(IServiceCollection services, IConfiguration configuration)
        {
            RegisterRefit(services, configuration);
        }

        private IServiceCollection RegisterRefit(IServiceCollection services, IConfiguration configuration)
        {
            var adminApiUrl = configuration.GetValue<string>("AdminApiUrl");

            if(string.IsNullOrEmpty(adminApiUrl))
                throw new Exception("AdminApiUrl is not configured");

            services.AddRefitClient<IAdminApiService>()
                .ConfigureHttpClient(c => c.BaseAddress = new Uri(adminApiUrl));

            return services;
        }
    }
}
