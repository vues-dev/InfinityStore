using InfinityStoreAdmin.Api.Shared.Entities;

namespace InfinityStoreAdmin.Api.Features.GetGames;

public class GetGamesResponse
{
    public List<Game> Games { get; set; } = new();
    public int TotalGames { get; set; }
}