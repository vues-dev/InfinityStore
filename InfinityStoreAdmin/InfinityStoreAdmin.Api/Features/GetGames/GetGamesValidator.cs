using FluentValidation;

namespace InfinityStoreAdmin.Api.Features.GetGames;

public class GetGamesValidator : AbstractValidator<GetGamesQuery>
{
    public GetGamesValidator()
    {
        RuleFor(x => x.CurrentPage).GreaterThanOrEqualTo(0);
        RuleFor(x => x.ItemsPerPage).GreaterThanOrEqualTo(1);
    }
}