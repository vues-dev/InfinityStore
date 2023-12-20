using System;
using System.ComponentModel;

namespace InfinityStoreAdmin.Api.Application.Games.AddGame
{
    public record GetGamesRequest(string SearchString,
                                  bool? IsTitleUp,
                                  bool? IsPriceUp,
                                  int CurrentPage = 1,
                                  int ItemsPerPage = 4);  
}