using MediatR;

namespace InfinityStoreAdmin.Api.Features.EditGame;

public class EditGameCommand : IRequest<Unit>
{
    public Guid Id { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
    public string ImagePath { get; set; }
    public decimal Price { get; set; }
}