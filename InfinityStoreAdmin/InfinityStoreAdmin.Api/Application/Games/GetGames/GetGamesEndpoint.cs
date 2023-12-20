using System;
using InfinityStoreAdmin.Api.Application.Games.GetGames;
using InfinityStoreAdmin.Api.Infrastructure.Database;
using InfinityStoreAdmin.Api.Shared;
using InfinityStoreAdmin.Api.Shared.Configurations;
using Microsoft.EntityFrameworkCore;
using Vues.Net;
using Vues.Net.Models;

namespace InfinityStoreAdmin.Api.Application.Games.AddGame;


public class GetGamesEndpoint : IGroupedEndpoint
{
    public string ApiGroup => ApiPaths.PATH_GAMES;

    public void DefineEndpoint(RouteGroupBuilder app)
    {
        app.MapGet("/", HandleAsync)
           .ValidateRequest<AddGameRequest>()
           .Produces<ValidationError>(StatusCodes.Status422UnprocessableEntity)
           .Produces<GetGamesResponse>(StatusCodes.Status200OK)
           .WithOpenApi(op => new(op)
           {
               Description = "",
               Summary = "Get game list",
               Tags = SwaggerConfig.GAMES_TAG
           });
    }

    private async Task<IResult> HandleAsync([AsParameters] GetGamesRequest request, DatabaseContext dbContext, CancellationToken cancellationToken)
    {
        var games = dbContext.Games.AsQueryable();

        var totalGames = games.Count();

        if (!string.IsNullOrEmpty(request.SearchString))
        {
            games = games.Where(x => x.Title.Contains(request.SearchString, StringComparison.OrdinalIgnoreCase));
        }

        if (request.IsTitleUp.HasValue)
        {
            games = request.IsTitleUp.Value
                ? games.OrderBy(x => x.Title)
                : games.OrderByDescending(x => x.Title);
        }

        if (request.IsPriceUp.HasValue)
        {
            games = request.IsPriceUp.Value
                ? games.OrderBy(x => x.Price)
                : games.OrderByDescending(x => x.Price);
        }

        var resultList = await games
            .Skip((request.CurrentPage - 1) * request.ItemsPerPage)
            .Take(request.ItemsPerPage)
            .OrderBy(x => x.Title)
            .ToArrayAsync(cancellationToken);

        var response = new GetGamesResponse(){
            Games= resultList,
            TotalGames= totalGames
        };

        return Results.Ok(response);
    }
}