using System;
using InfinityStoreAdmin.Api.Infrastructure.Database;

namespace InfinityStoreAdmin.Api.IntegrationTests
{
    public class SutFactory<TProgram> : AbstractSutFactory<Program, DatabaseContext>
    {
        protected override string ConnectionString =>
            "User ID=postgres;Password=1234;Host=localhost;Port=5432;Database=infinity_store_admin_db_test;";
    }
}

