using MediatR;

namespace InfinityStoreAdmin.Api.Features.AddGame;

public class AddGameCommand : IRequest<Unit>
{
    public string Title { get; set; }
    public string Description { get; set; }
    public string ImageUrl { get; set; }
    public decimal Price { get; set; }
}