namespace InfinityStoreAdmin.BlazorApp.Services.Models;

public class GameEntity
{
    public Guid Id { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
    public decimal Price { get; set; }
    public string Image { get; set; }
}