namespace InfinityStoreAdmin.Api.Shared.Configurations;

public interface IConfigurationSetup
{
    void ConfigureAll(IServiceCollection services, IConfiguration configuration);
}