using System.Net;
using System.Net.Http.Json;
using InfinityStoreAdmin.Api.Application.Games.AddGame;
using InfinityStoreAdmin.Api.Application.Games.EditGame;
using InfinityStoreAdmin.Api.Shared;
using InfinityStoreAdmin.Api.Shared.Entities;
using InfinityStoreAdmin.Api.VuesInfrastructure.Extensions;
using InfinityStoreAdmin.Api.VuesInfrastructure.Models;
using Microsoft.EntityFrameworkCore;

namespace InfinityStoreAdmin.Api.IntegrationTests.Application.Games;

[Collection("Sut Tests Collection")]
public class EditGameTests
{
    private readonly SutFactory<Program> _sut;

    public EditGameTests(SutFactory<Program> sut)
    {
        _sut = sut;
    }

    [Fact]
    public async Task EditGame_IncorrectData_ShouldReturn422()
    {
        // Arrange
        var payload = new EditGameRequest(Title: string.Empty, Description: "desk", ImagePath: null, Price: 0);
        using var client = _sut.CreateClient();

        // Act
        var result = await client.PutAsJsonAsync($"{ApiPaths.PATH_GAMES}/{Guid.NewGuid()}", payload);
        var content = await result.Content.ReadFromJsonAsync<ValidationError>();
        // Assert
        Assert.Equal(HttpStatusCode.UnprocessableEntity, result.StatusCode);
        Assert.True(content!.Errors.ContainsKey("Title"));
        Assert.True(content.Errors.ContainsKey("ImagePath"));
    }

    [Fact]
    public async Task EditGame_GameNotFound_ShouldReturn404()
    {
        // Arrange
        var payload = new EditGameRequest(Title: "title", Description: "desk", ImagePath: "path", Price: 0);
        using var client = _sut.CreateClient();

        // Act
        var result = await client.PutAsJsonAsync($"{ApiPaths.PATH_GAMES}/{Guid.NewGuid()}", payload);

        // Assert
        Assert.Equal(HttpStatusCode.NotFound, result.StatusCode);
    }

    [Fact]
    public async Task EditGame_CorrectData_ShouldReturn200()
    {
        // Arrange
        var game = new Game
        {
            Title = "title",
            Description = "desk",
            ImagePath = "path",
            Price = 0
        };

        await _sut.DbContext.Games.InsertAsync(game);
        var payload = new EditGameRequest(Title: "title2", Description: "desk2", ImagePath: "path2", Price: 1);
        using var client = _sut.CreateClient();

        // Act
        var result = await client.PutAsJsonAsync($"{ApiPaths.PATH_GAMES}/{game.Id}", payload);

        // Assert
        var gameInDb = await _sut.DbContext.Games.FirstOrDefaultAsync(g => g.Id == game.Id);

        Assert.NotNull(gameInDb);
        Assert.Equal(payload.Title, gameInDb.Title);
        Assert.Equal(payload.Description, gameInDb.Description);
        Assert.Equal(payload.ImagePath, gameInDb.ImagePath);
        Assert.Equal(payload.Price, gameInDb.Price);
    }
}