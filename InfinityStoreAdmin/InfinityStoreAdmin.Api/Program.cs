
using FluentValidation;
using InfinityStoreAdmin.Api.Features.AddGame;
using InfinityStoreAdmin.Api.Shared.Behaviors;
using InfinityStoreAdmin.Api.Shared.Configurations;
using InfinityStoreAdmin.Api.Shared.Extensions;
using InfinityStoreAdmin.Api.Shared.FrameworkCustomizing.OperationGroup;
using InfinityStoreAdmin.Api.Shared.Middleware;
using MediatR;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;

namespace InfinityStoreAdmin.Api
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers(options =>
            {
                options.Conventions.Add(new OperationGroupConvention());
            });

            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            // configure app (settings, dependencies, etc)
            IConfigurationSetup appSettingSetup = new AppConfigurationSetup();
            appSettingSetup.ConfigureAll(builder.Services, builder.Configuration);

            var app = builder.Build();

            app.UseSwagger();
            app.UseSwaggerUI();

            app.UseMiddleware<ExceptionHandlingMiddleware>();

            //app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
