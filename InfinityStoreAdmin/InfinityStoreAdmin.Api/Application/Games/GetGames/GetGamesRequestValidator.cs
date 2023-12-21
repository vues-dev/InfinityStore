using FluentValidation;

namespace InfinityStoreAdmin.Api.Application.Games.GetGames
{
    public class GetGamesRequestValidator : AbstractValidator<GetGamesRequest>
    {
        public GetGamesRequestValidator()
        {
            RuleFor(x => x.CurrentPage).GreaterThanOrEqualTo(1);
            RuleFor(x => x.ItemsPerPage).GreaterThanOrEqualTo(1);
        }

    } 
}

