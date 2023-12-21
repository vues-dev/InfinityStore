namespace InfinityStoreAdmin.Api.Application.Games.EditGame
{
    public record EditGameRequest(string Title,
                                  string Description,
                                  string ImagePath,
                                  decimal Price);  
}

