using Asp.Versioning;
using Asp.Versioning.Builder;
using Asp.Versioning.Conventions;
using InfinityStoreAdmin.Api.Shared;
using InfinityStoreAdmin.Api.Shared.Configurations;
using InfinityStoreAdmin.Api.Shared.FrameworkCustomizing.OperationGroup;
using InfinityStoreAdmin.Api.Shared.Middleware;
using InfinityStoreAdmin.Api.VuesInfrastructure.Extensions;
using Microsoft.OpenApi.Models;


var apiVersions = new List<ApiVersion>()
{
    new ApiVersion(1.0),
    new ApiVersion(2.0)
};

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers(options =>
{
    options.Conventions.Add(new OperationGroupConvention());
});

builder.Services.AddApiVersioning(options =>
{
    options.DefaultApiVersion = new ApiVersion(1,0);
    options.ReportApiVersions = true;
    options.AssumeDefaultVersionWhenUnspecified = true;
    options.ApiVersionReader = ApiVersionReader.Combine(
        new UrlSegmentApiVersionReader(),
        new HeaderApiVersionReader("X-Api-Version"));
}).AddApiExplorer(options =>
{
    options.GroupNameFormat = "'v'VV";
});



// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c=>{
    foreach(var apiVersion in apiVersions)
            {
                 c.SwaggerDoc($"v{apiVersion.MajorVersion}.{apiVersion.MinorVersion}", new OpenApiInfo()
                {
                    Title =  "Some of the Workspace API",
                    Version = $"{apiVersion.MajorVersion}.{apiVersion.MinorVersion}",
                    Description = "Описание API не заполнено",
                });
            }
});

// configure app (settings, dependencies, etc)
IConfigurationSetup appSettingSetup = new AppConfigurationSetup();
appSettingSetup.ConfigureAll(builder.Services, builder.Configuration);

var app = builder.Build();

ApiVersionSet apiVersionSet = app.NewApiVersionSet()
                                 .HasApiVersions(apiVersions)
                                 .ReportApiVersions()
                                 .Build();
    
app.UseMiddleware<ExceptionHandlingMiddleware>();

//app.UseHttpsRedirection();

app.UseAuthorization();


app.MapControllers();
app.RegisterEndpoints(new ApiPaths(), apiVersionSet);

app.UseSwagger();
app.UseSwaggerUI(opts =>
{
        var descriptions = app.DescribeApiVersions();
        foreach (var desc in descriptions)
        {
            var url = $"/swagger/{desc.GroupName}/swagger.json";
            var name = desc.GroupName.ToUpperInvariant();
            opts.SwaggerEndpoint(url, $"{name}");
        }
});



app.Run();

namespace InfinityStoreAdmin.Api
{
    public partial class Program { }
}