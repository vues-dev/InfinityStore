using System;
using InfinityStoreAdmin.Api.Infrastructure.Database;
using InfinityStoreAdmin.Api.Shared.Entities;

namespace InfinityStoreAdmin.Api.IntegrationTests
{
    [CollectionDefinition("Heavy Tests Collection")]
    public class HeavyTestCollection : ICollectionFixture<SutFactory<Program>>
    {

    }

    public class SutFactory<TProgram> : AbstractSutFactory<Program, DatabaseContext>
    {
        protected override string ConnectionString =>
            "User ID=postgres;Password=1234;Host=localhost;Port=5432;Database=infinity_store_admin_db_test;";

        protected override void PrepareTestData()
        {
            var ctx = CreateDbContext();

            ctx.Database.EnsureDeleted();
            ctx.Database.EnsureCreated();

            var games = new List<Game>()
            {
                new Game()
                {
                    Title = "search substring game",
                    Description = "desk",
                    ImagePath = "path",
                    Price = 30
                },
                new()
                {
                    Title = "A game",
                    Description = "desk",
                    ImagePath = "path",
                    Price = 30
                },
                new()
                {
                    Title = "B game",
                    Description = "desk",
                    ImagePath = "path",
                    Price = 20
                },
                new()
                {
                    Title = "C game",
                    Description = "desk",
                    ImagePath = "path",
                    Price = 10
                },
                new()
                {
                    Title = "D game",
                    Description = "desk",
                    ImagePath = "path",
                    Price = 15
                },
                new()
                {
                    Title = "E game",
                    Description = "desk",
                    ImagePath = "path",
                    Price = 25
                },
                new()
                {
                    Title = "F game",
                    Description = "desk",
                    ImagePath = "path",
                    Price = 5
                },
                new()
                {
                    Title = "G game",
                    Description = "desk",
                    ImagePath = "path",
                    Price = 35
                },
                new()
                {
                    Title = "H game",
                    Description = "desk",
                    ImagePath = "path",
                    Price = 40
                },
                new() {
                    Title = "I game",
                    Description = "desk",
                    ImagePath = "path",
                    Price = 45
                },
                new() {
                    Title = "J game",
                    Description = "desk",
                    ImagePath = "path",
                    Price = 50
                }
            };

            ctx.Games.AddRange(games);
            ctx.SaveChanges();   
        }
    }
}

