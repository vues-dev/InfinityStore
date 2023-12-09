namespace InfinityStoreAdmin.BlazorApp.Services.Models;

public class GetGamesResponse
{
    public List<GameModel> Games { get; set; } = new();

    public int TotalGames { get; set; }
}