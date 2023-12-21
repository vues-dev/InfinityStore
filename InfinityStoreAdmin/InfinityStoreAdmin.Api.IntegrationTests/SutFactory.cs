using System;
using InfinityStoreAdmin.Api.Infrastructure.Database;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace InfinityStoreAdmin.Api.IntegrationTests
{
    public class SutFactory<TProgram> : WebApplicationFactory<TProgram> where TProgram : class
    {
        public DatabaseContext DbContext => _lazyDbContext.Value;

        private readonly Lazy<DatabaseContext> _lazyDbContext;
        private IServiceScope _serviceScope;

        public SutFactory()
        {
            _lazyDbContext = new Lazy<DatabaseContext>(CreateDbContext);
        }

        private DatabaseContext CreateDbContext()
        {
            _serviceScope = Services.CreateScope();
            return _serviceScope.ServiceProvider.GetRequiredService<DatabaseContext>();
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing && _lazyDbContext.IsValueCreated)
            {
                _serviceScope.Dispose();
            }

            base.Dispose(disposing);
        }
    }
}

