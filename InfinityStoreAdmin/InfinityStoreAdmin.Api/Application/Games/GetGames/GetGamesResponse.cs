using System;
using InfinityStoreAdmin.Api.Shared.Entities;

namespace InfinityStoreAdmin.Api.Application.Games.GetGames
{
    public class GetGamesResponse
    {
        public required Game[] Games { get; init; }
        public required int TotalGames { get; init; }
    }
        
}

