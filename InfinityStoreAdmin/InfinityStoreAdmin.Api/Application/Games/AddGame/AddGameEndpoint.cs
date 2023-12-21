using InfinityStoreAdmin.Api.Infrastructure.Database;
using InfinityStoreAdmin.Api.Shared;
using InfinityStoreAdmin.Api.Shared.Configurations;
using InfinityStoreAdmin.Api.Shared.Entities;
using InfinityStoreAdmin.Api.VuesInfrastructure.Endpoints;
using InfinityStoreAdmin.Api.VuesInfrastructure.Extensions;
using InfinityStoreAdmin.Api.VuesInfrastructure.Models;

namespace InfinityStoreAdmin.Api.Application.Games.AddGame;


public class AddGameEndpoint : IGroupedEndpoint
{
    public string ApiGroup => ApiPaths.PATH_GAMES;

    public void DefineEndpoint(RouteGroupBuilder app)
    {
        app.MapPost("/", HandleAsync)
           .ValidateRequest()
           .Produces<ValidationError>(StatusCodes.Status422UnprocessableEntity)
           .Produces<Guid>(StatusCodes.Status200OK)
           .WithOpenApi(op => new(op)
           {
               Description = "",
               Summary = "Add game",
               Tags = SwaggerConfig.GAMES_TAG
           });
    }

    private async Task<IResult> HandleAsync(AddGameRequest request, DatabaseContext dbContext, CancellationToken cancellationToken)
    {
        var game = new Game
        {
            Title = request.Title,
            Description = request.Description,
            ImagePath = request.ImagePath,
            Price = request.Price
        };

        await dbContext.Games.InsertAsync(game, cancellationToken);

        return Results.Ok(game.Id);
    }
}