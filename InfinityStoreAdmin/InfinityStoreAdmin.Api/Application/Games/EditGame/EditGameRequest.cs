using System;
namespace InfinityStoreAdmin.Api.Application.Games.AddGame
{
    public record EditGameRequest(string Title,
                                  string Description,
                                  string ImagePath,
                                  decimal Price);  
}

