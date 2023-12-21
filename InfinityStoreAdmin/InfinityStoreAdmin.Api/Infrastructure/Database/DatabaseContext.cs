using InfinityStoreAdmin.Api.Shared.Entities;
using Microsoft.EntityFrameworkCore;

namespace InfinityStoreAdmin.Api.Infrastructure.Database
{
    public class DatabaseContext : DbContext
    {
        public DbSet<Game> Games { get; set; }

        public DatabaseContext()
        {
            
        }

        public DatabaseContext(DbContextOptions<DatabaseContext> options) : base(options)
        {
            Database.EnsureCreated();
        }
    }
}
