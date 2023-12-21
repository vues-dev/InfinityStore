using System.Net;
using System.Net.Http.Json;
using InfinityStoreAdmin.Api.Application.Games.GetGames;
using InfinityStoreAdmin.Api.Shared;
using InfinityStoreAdmin.Api.Shared.Entities;
using InfinityStoreAdmin.Api.VuesInfrastructure.Extensions;
using InfinityStoreAdmin.Api.VuesInfrastructure.Models;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

namespace InfinityStoreAdmin.Api.IntegrationTests.Application.Games;

[Collection("Sut Tests Collection")]
public class GetGamesTests 
{
    private readonly SutFactory _sut;

    public GetGamesTests(SutFactory sut)
    {
        _sut = sut;
    }

    private async Task SeedDataIfNeeded()
    {
      
    }

    [Fact]
    public async Task GetGames_NoGames_ShouldReturnEmptyList()
    {
        // Arrange
        using var client = _sut.CreateClient();

        // Act
        var result = await client.GetFromJsonAsync<GetGamesResponse>($"{ApiPaths.PATH_GAMES}");

        // Assert
        Assert.NotNull(result);

        if (!_sut.DbContext.Games.Any())
        {
            Assert.Empty(result.Games);
        }

    }

    [Fact]
    public async Task GetGames_WithSearchString_ShouldReturnFilteredGames()
    {
        // Arrange
        using var client = _sut.CreateClient();
        var searchString = "search substring";

        var gamesWithSeacrhStringCount = await _sut.DbContext.Games.CountAsync(g => EF.Functions.Like(g.Title, $"%{searchString}%"));

        if (gamesWithSeacrhStringCount == 0)
        {
            await _sut.DbContext.Games.InsertAsync(new()
            {
                Title = "search substring game",
                Description = "desk",
                ImagePath = "path",
                Price = 30
            });
        }


        // Act
        var result = await client.GetFromJsonAsync<GetGamesResponse>($"{ApiPaths.PATH_GAMES}?SearchString={searchString}");

        Assert.NotEmpty(result.Games);
        Assert.All(result.Games, game => Assert.Contains(searchString, game.Title, StringComparison.OrdinalIgnoreCase));

    }

    [Fact]
    public async Task GetGames_WithPagination_ShouldReturnPaginatedGames()
    {
        // Arrange
        // Add more games than the default page size
        using var client = _sut.CreateClient();
        var currentPage = 2;
        var itemsPerPage = 5;

        // Act
        var result = await client.GetFromJsonAsync<GetGamesResponse>($"{ApiPaths.PATH_GAMES}?CurrentPage={currentPage}&ItemsPerPage={itemsPerPage}");

        // Assert
        Assert.NotNull(result);
        Assert.Equal(itemsPerPage, result.Games.Length);
        // Additional asserts to check if the games are the expected ones based on pagination
    }

    [Fact]
    public async Task GetGames_SortByTitleAsc_ShouldReturnGamesInAscendingOrder()
    {
        // Arrange
        using var client = _sut.CreateClient();
        var isTitleUp = true; // Sorting by title in ascending order

        // Act
        var result = await client.GetFromJsonAsync<GetGamesResponse>($"{ApiPaths.PATH_GAMES}?IsTitleUp={isTitleUp}");

        // Assert
        Assert.NotNull(result);
        Assert.True(result.Games.Length > 1, "Should have more than one game for sorting test.");
        // Ensure that the games are sorted by title in ascending order
        Assert.Equal(result.Games, result.Games.OrderBy(g => g.Title).ToArray());
    }

    [Fact]
    public async Task GetGames_SortByPriceDesc_ShouldReturnGamesInDescendingOrder()
    {
        // Arrange
        using var client = _sut.CreateClient();
        var isPriceUp = false; // Sorting by price in descending order

        // Act
        var result = await client.GetFromJsonAsync<GetGamesResponse>($"{ApiPaths.PATH_GAMES}?IsPriceUp={isPriceUp}");

        // Assert
        Assert.NotNull(result);
        Assert.True(result.Games.Length > 1, "Should have more than one game for sorting test.");
        // Ensure that the games are sorted by price in descending order
        Assert.Equal(result.Games, result.Games.OrderByDescending(g => g.Price).ToArray());
    }

    [Fact]
    public async Task GetGames_SortByTitleAndPrice_ShouldReturnBadRequest()
    {
        // Arrange
        using var client = _sut.CreateClient();
        var isTitleUp = true; // Sorting by title in ascending order
        var isPriceUp = true; // Sorting by price in ascending order
        // Act
        var result = await client.GetAsync($"{ApiPaths.PATH_GAMES}?IsTitleUp={isTitleUp}&IsPriceUp={isPriceUp}");
        // Assert
        Assert.Equal(HttpStatusCode.BadRequest, result.StatusCode);
    }

    [Fact]
    public async Task GetGames_WithInvalidPagination_ShouldReturnUnprocessableEntity()
    {
        // Arrange
        using var client = _sut.CreateClient();
        var currentPage = 0;
        var itemsPerPage = 0;
        // Act
        var result = await client.GetAsync($"{ApiPaths.PATH_GAMES}?CurrentPage={currentPage}&ItemsPerPage={itemsPerPage}");
        var content = await result.Content.ReadFromJsonAsync<ValidationError>();
        // Assert
        Assert.Equal(HttpStatusCode.UnprocessableEntity, result.StatusCode);
        Assert.True(content!.Errors.ContainsKey("CurrentPage"));
        Assert.True(content.Errors.ContainsKey("ItemsPerPage"));
    }
}