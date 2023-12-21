using FluentValidation;
using InfinityStoreAdmin.Api.Features.AddGame;
using InfinityStoreAdmin.Api.Infrastructure.Database;
using InfinityStoreAdmin.Api.Infrastructure.Repositories;
using InfinityStoreAdmin.Api.Shared.Behaviors;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace InfinityStoreAdmin.Api.Shared.Configurations
{
    public class AppConfigurationSetup : IConfigurationSetup
    {
        public void ConfigureAll(IServiceCollection services, IConfiguration configuration)
        {
            AddDbContext(services, configuration);
            RegisterApplicationDependencies(services);
            RegisterServices(services);
            RegisterValidation(services);
        }

        private IServiceCollection AddDbContext(IServiceCollection services, IConfiguration configuration)
        {
            var connectionString = configuration.GetConnectionString("InfinityStoreAdminDb");

            services.AddDbContext<DatabaseContext>(options => options.UseNpgsql(connectionString));

            return services;
        }

        private IServiceCollection RegisterApplicationDependencies(IServiceCollection services)
        {
            services.AddAutoMapper(Assembly.GetExecutingAssembly());
            services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblies(Assembly.GetExecutingAssembly()));

            return services;
        }

        private IServiceCollection RegisterServices(IServiceCollection services)
        {
            services.AddScoped<IGameRepository, GameRepository>();

            return services;
        }

        private IServiceCollection RegisterValidation(IServiceCollection services)
        {
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));
            services.AddValidatorsFromAssemblyContaining<AddGameValidator>();
            return services;
        }
    }
}
