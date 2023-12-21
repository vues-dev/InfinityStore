namespace InfinityStoreAdmin.Api.Application.Games.GetGames
{
    public record GetGamesRequest(string? SearchString,
                                  bool? IsTitleUp,
                                  bool? IsPriceUp,
                                  int CurrentPage = 1,
                                  int ItemsPerPage = 4);  
}