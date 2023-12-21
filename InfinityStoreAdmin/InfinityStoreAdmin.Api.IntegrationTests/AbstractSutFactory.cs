using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace InfinityStoreAdmin.Api.IntegrationTests;

public abstract class AbstractSutFactory<TProgram, TContext> : WebApplicationFactory<TProgram> 
    where TProgram : class 
    where TContext : DbContext, new()
{
    public TContext DbContext => CreateDbContext();

    protected abstract string ConnectionString { get; }

    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        builder.ConfigureServices(services =>
        {
            var dbContextDescriptor = services.SingleOrDefault(d => d.ServiceType == typeof(DbContextOptions<TContext>));
            services.Remove(dbContextDescriptor!);

            services.AddDbContext<TContext>(options => NpgsqlDbContextOptionsBuilderExtensions.UseNpgsql(options, ConnectionString));
        });

        PrepareTestData();
    }

    public TContext CreateDbContext()
    {
        var options = new DbContextOptionsBuilder<TContext>()
            .UseNpgsql(ConnectionString)
            .Options;

        return (TContext)Activator.CreateInstance(typeof(TContext), options)!;
    }

    protected abstract void PrepareTestData();
}