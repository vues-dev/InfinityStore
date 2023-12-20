using System;
using InfinityStoreAdmin.Api.Infrastructure.Database;
using InfinityStoreAdmin.Api.Shared;
using InfinityStoreAdmin.Api.Shared.Configurations;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Vues.Net;
using Vues.Net.Models;

namespace InfinityStoreAdmin.Api.Application.Games.AddGame;


public class EditGameEndpoint : IGroupedEndpoint
{
    public string ApiGroup => ApiPaths.PATH_GAMES;

    public void DefineEndpoint(RouteGroupBuilder app)
    {
        app.MapPut("/{gameId}", HandleAsync)
           .ValidateRequest<EditGameRequest>()
           .Produces<ValidationError>(StatusCodes.Status422UnprocessableEntity)
           .Produces<string>(StatusCodes.Status404NotFound)
           .Produces<Guid>(StatusCodes.Status200OK)
           .WithOpenApi(op => new(op)
           {
               Description = "",
               Summary = "Edit game",
               Tags = SwaggerConfig.GAMES_TAG
           });
    }

    private async Task<IResult> HandleAsync([FromRoute] Guid gameId, EditGameRequest request, DatabaseContext dbContext, CancellationToken cancellationToken)
    {
        var game = await dbContext.Games.FirstOrDefaultAsync(x=> x.Id == gameId, cancellationToken);

        if (game is null)
        {
            return Results.NotFound($"Game with id {gameId} was not found.");
        }

        game.Title = request.Title;
        game.Description = request.Description;
        game.ImagePath = request.ImagePath;
        game.Price = request.Price;

        await dbContext.Games.UpdateAsync(game, cancellationToken);

        return Results.Ok(gameId);
    }
}