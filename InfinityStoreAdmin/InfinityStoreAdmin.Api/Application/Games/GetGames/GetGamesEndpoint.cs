using InfinityStoreAdmin.Api.Infrastructure.Database;
using InfinityStoreAdmin.Api.Shared;
using InfinityStoreAdmin.Api.Shared.Configurations;
using InfinityStoreAdmin.Api.VuesInfrastructure.Endpoints;
using InfinityStoreAdmin.Api.VuesInfrastructure.Extensions;
using InfinityStoreAdmin.Api.VuesInfrastructure.Models;
using Microsoft.EntityFrameworkCore;

namespace InfinityStoreAdmin.Api.Application.Games.GetGames;


public class GetGamesEndpoint : IGroupedEndpoint
{
    public string ApiGroup => ApiPaths.PATH_GAMES;

    public void DefineEndpoint(RouteGroupBuilder app)
    {
        app.MapGet("/", HandleAsync)
           .ValidateRequest()
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

        if (request.IsTitleUp.HasValue && request.IsPriceUp.HasValue)
        {
            return Results.BadRequest("Cannot sort by title and price at the same time");
        }

        if (!string.IsNullOrEmpty(request.SearchString))
        {
            games = games.Where(x => EF.Functions.Like(x.Title, $"%{request.SearchString}%"));
        }

        // Apply sorting based on the provided parameter, only one will take effect
        if (request.IsTitleUp.HasValue)
        {
            games = request.IsTitleUp.Value
                ? games.OrderBy(x => x.Title)
                : games.OrderByDescending(x => x.Title);
        }
        else if (request.IsPriceUp.HasValue) // 'else if' ensures only one sorting is applied
        {
            games = request.IsPriceUp.Value
                ? games.OrderBy(x => x.Price)
                : games.OrderByDescending(x => x.Price);
        }
        else
        {
            // If no sorting parameter is provided, apply a default sorting
            games = games.OrderBy(x => x.Title);
        }

        var skipAmount = (request.CurrentPage - 1) * request.ItemsPerPage;

        var resultList = await games
            .Skip(skipAmount)
            .Take(request.ItemsPerPage)
            .ToArrayAsync(cancellationToken);

        var response = new GetGamesResponse()
        {
            Games = resultList,
            TotalGames = totalGames
        };

        return Results.Ok(response);
    }
}