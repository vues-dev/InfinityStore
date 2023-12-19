using InfinityStoreAdmin.Api.Infrastructure.Repositories;
using InfinityStoreAdmin.Api.Shared.Entities;
using MediatR;

namespace InfinityStoreAdmin.Api.Features.AddGame;

public class AddGameControllerHandler(IGameRepository gameRepository) : IRequestHandler<AddGameCommand, Unit>
{
    public async Task<Unit> Handle(AddGameCommand request, CancellationToken cancellationToken)
    {
        var game = new Game
        {
            Title = request.Title,
            Description = request.Description,
            ImagePath = request.ImagePath,
            Price = request.Price
        };

        await gameRepository.AddAsync(game);

        return Unit.Value;
    }
}