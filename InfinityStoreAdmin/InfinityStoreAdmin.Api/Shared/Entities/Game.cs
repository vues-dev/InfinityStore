namespace InfinityStoreAdmin.Api.Shared.Entities
{
    public class Game
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string ImagePath { get; set; } 
        public decimal Price { get; set; }
    }
}
