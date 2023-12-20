using System;
using InfinityStoreAdmin.Api.Shared;
using Microsoft.AspNetCore.Identity.Data;
using System.Net;
using System.Net.Http.Json;
using InfinityStoreAdmin.Api.Application.Games.AddGame;
using Vues.Net.Models;

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
    }
}

