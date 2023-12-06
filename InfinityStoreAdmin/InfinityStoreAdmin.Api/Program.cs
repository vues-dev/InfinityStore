
using InfinityStoreAdmin.Api.Shared.Configurations;
using InfinityStoreAdmin.Api.Shared.Extensions;
using InfinityStoreAdmin.Api.Shared.FrameworkCustomizing.OperationGroup;

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

            // register app dependencies
            builder.Services.AddApplication();
            builder.Services.RegisterServices();

            // configure app settings
            IConfigurationSetup appSettingSetup = new AppSettingsConfigurationSetup();
            appSettingSetup.ConfigureAll(builder.Services, builder.Configuration);

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
