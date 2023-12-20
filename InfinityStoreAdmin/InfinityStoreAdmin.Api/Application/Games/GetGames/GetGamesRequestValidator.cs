using System;
using FluentValidation;

namespace InfinityStoreAdmin.Api.Application.Games.AddGame
{
    public class GetGamesRequestValidator : AbstractValidator<GetGamesRequest>
    {
        public GetGamesRequestValidator()
        {
            RuleFor(x => x.CurrentPage).GreaterThanOrEqualTo(0);
            RuleFor(x => x.ItemsPerPage).GreaterThanOrEqualTo(1);
        }

    } 
}

