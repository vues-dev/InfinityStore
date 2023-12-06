using InfinityStoreAdmin.Api.Infrastructure.Repositories;
using InfinityStoreAdmin.Api.Shared.Entities;
using MediatR;

namespace InfinityStoreAdmin.Api.Features.AddGame;

public class AddGameControllerHandler : IRequestHandler<AddGameCommand, Unit>
{
    private readonly IGameRepository _gameRepository;

    public AddGameControllerHandler(IGameRepository gameRepository)
    {
        _gameRepository = gameRepository;
    }

    public async Task<Unit> Handle(AddGameCommand request, CancellationToken cancellationToken)
    {
        var game = new Game
        {
            Title = request.Title,
            Description = request.Description,
            ImageUrl = request.ImageUrl,
            Price = request.Price
        };



        await _gameRepository.AddAsync(game);

        return Unit.Value;
    }
}