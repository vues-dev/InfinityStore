using InfinityStoreAdmin.Api.Infrastructure.Database;
using Microsoft.EntityFrameworkCore;

namespace InfinityStoreAdmin.Api.Shared.Configurations
{
    public class AppSettingsConfigurationSetup : IConfigurationSetup
    {
        public void ConfigureAll(IServiceCollection services, IConfiguration configuration)
        {
            AddDbContext(services, configuration);
        }

        private IServiceCollection AddDbContext(IServiceCollection services, IConfiguration configuration)
        {
            var connectionString = configuration.GetSection(Constants.DatabaseSection).Get<DatabaseConnectionString>();

            services.AddDbContext<DatabaseContext>(options => options.UseNpgsql(connectionString.ToString()));

            return services;
        }
    }
}
