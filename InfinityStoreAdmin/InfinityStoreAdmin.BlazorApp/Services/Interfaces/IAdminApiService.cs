using InfinityStoreAdmin.BlazorApp.Services.Models;
using Refit;

namespace InfinityStoreAdmin.BlazorApp.Services.Interfaces
{
    [Headers("Accept: application/json")]
    public interface IAdminApiService
    {
        [Post("/store-admin-api/get-games")]
        Task<GetGamesResponse> GetGamesAsync([Body] GetGamesRequest request);

        //[Post("/api/admin/games")]
        //Task CreateGame(GameModel game);

        //[Put("/api/admin/games")]
        //Task UpdateGame(GameModel game);
    }


}
