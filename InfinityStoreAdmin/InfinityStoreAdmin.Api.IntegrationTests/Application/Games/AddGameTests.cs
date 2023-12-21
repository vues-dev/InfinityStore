using System;
using InfinityStoreAdmin.Api.Shared;
using Microsoft.AspNetCore.Identity.Data;
using System.Net;
using System.Net.Http.Json;
using InfinityStoreAdmin.Api.Application.Games.AddGame;
using Vues.Net.Models;
using Microsoft.EntityFrameworkCore;

namespace InfinityStoreAdmin.Api.IntegrationTests.Application.Games
{
    public class AddGameTests: IClassFixture<SutFactory<Program>>
    {
        private readonly SutFactory<Program> _sut;

        public AddGameTests(SutFactory<Program> sut)
        {
            _sut = sut;
        }

        [Fact]
        public async Task AddGame_IncorrectData_ShouldReturn422()
        {
            // Arrange
            var payload = new AddGameRequest(Title: string.Empty, Description: "desk", ImagePath: null, Price: 0);
            using var client = _sut.CreateClient();

            // Act
            var result = await client.PostAsJsonAsync($"{ApiPaths.PATH_GAMES}", payload);

            // Assert
            Assert.Equal(HttpStatusCode.UnprocessableEntity, result.StatusCode);
        }


        //success
        [Fact]
        public async Task AddGame_CorrectData_ShouldReturn200()
        {
            // Arrange
            var payload = new AddGameRequest(Title: "title", Description: "desk", ImagePath: "path", Price: 0);
            using var client = _sut.CreateClient();
            var db = _sut.CreateDbContext();

            // Act
            var result = await client.PostAsJsonAsync($"{ApiPaths.PATH_GAMES}", payload);
            var content = await result.Content.ReadFromJsonAsync<Guid>();
            var gameExistInDb = await db.Games.AnyAsync(x => x.Id == content);

            // Assert
            Assert.Equal(HttpStatusCode.OK, result.StatusCode);
            Assert.True(gameExistInDb);
        }
    }
}

