using System;
using InfinityStoreAdmin.Api.Shared;
using Microsoft.AspNetCore.Identity.Data;
using System.Net;
using System.Net.Http.Json;
using InfinityStoreAdmin.Api.Application.Games.AddGame;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

namespace InfinityStoreAdmin.Api.IntegrationTests.Application.Games
{
    [Collection("Heavy Tests Collection")]
    public class AddGameTests
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
            // Act
            var result = await client.PostAsJsonAsync($"{ApiPaths.PATH_GAMES}", payload);

            // Assert
            Assert.Equal(HttpStatusCode.OK, result.StatusCode);

            var content = await result.Content.ReadAsStringAsync();
            var gameId = JsonConvert.DeserializeObject<Guid>(content);
            var game = await _sut.DbContext.Games.FirstOrDefaultAsync(g => g.Id == gameId);

            Assert.NotNull(game);
            Assert.Equal(payload.Title, game.Title);
            Assert.Equal(payload.Description, game.Description);
            Assert.Equal(payload.ImagePath, game.ImagePath);
            Assert.Equal(payload.Price, game.Price);
        }
    }
}

