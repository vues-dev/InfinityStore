using InfinityStoreAdmin.Api.Infrastructure.Repositories;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace InfinityStoreAdmin.Api.Features.GetGames;

public class GetGamesQueryHandler(IGameRepository gameRepository, ILogger<GetGamesQueryHandler> logger) : IRequestHandler<GetGamesQuery, GetGamesResult>
{
    public async Task<GetGamesResult> Handle(GetGamesQuery request, CancellationToken ct)
    {
        var games = gameRepository.Query();

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
            .OrderBy(x=>x.Title)
            .ToListAsync(ct);

        return new GetGamesResult
        {
            Games = resultList,
            TotalGames = totalGames
        };
    }
}