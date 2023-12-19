using InfinityStoreAdmin.Api.Infrastructure.Repositories;
using InfinityStoreAdmin.Api.Shared.Middleware.Exceptions;
using MediatR;

namespace InfinityStoreAdmin.Api.Features.EditGame;

public class EditGameCommandHandler(IGameRepository gameRepository) : IRequestHandler<EditGameCommand, Unit>
{
    public async Task<Unit> Handle(EditGameCommand request, CancellationToken ct)
    {
        var game = await gameRepository.GetAsync(request.Id, ct);

        if (game is null)
        {
            throw new NotFoundException($"Game with id {request.Id} was not found.");
        }

        game.Title = request.Title;
        game.Description = request.Description;
        game.ImagePath = request.ImagePath;
        game.Price = request.Price;

        await gameRepository.UpdateAsync(game);

        return Unit.Value;
    }
}