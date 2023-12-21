using InfinityStoreAdmin.Api.Shared;
using InfinityStoreAdmin.Api.Shared.Configurations;
using InfinityStoreAdmin.Api.Shared.FrameworkCustomizing.OperationGroup;
using InfinityStoreAdmin.Api.Shared.Middleware;
using InfinityStoreAdmin.Api.VuesInfrastructure.Extensions;

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
app.RegisterEndpoints(new ApiPaths());

app.Run();

namespace InfinityStoreAdmin.Api
{
    public partial class Program { }
}