using System;
using InfinityStoreAdmin.Api.Infrastructure.Database;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;

namespace InfinityStoreAdmin.Api.IntegrationTests
{
    public class SutFactory<TProgram> : WebApplicationFactory<TProgram> where TProgram : class
    {
        public SutFactory()
        {
        }

        public DatabaseContext CreateDbContext()
        {
            var options = new DbContextOptionsBuilder<DatabaseContext>()
            .UseNpgsql("User ID=user;Password=password;Host=postgres;Port=5432;Database=infinity_store_admin_db;")
            .Options;

            var db = new DatabaseContext(options);

            return db;
        }

    }
}

