using System;
namespace InfinityStoreAdmin.Api.Application.Games.AddGame
{
    public record AddGameRequest(string Title,
                                 string Description,
                                 string ImagePath,
                                 decimal Price);  
}

