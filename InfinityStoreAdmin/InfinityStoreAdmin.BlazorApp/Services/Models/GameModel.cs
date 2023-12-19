using System.ComponentModel.DataAnnotations;

namespace InfinityStoreAdmin.BlazorApp.Services.Models
{
    public class GameModel
    {
        public Guid Id { get; set; }

        [Required(ErrorMessage = "Title is required")]
        public string Title { get; set; }

        [Required(ErrorMessage = "Description is required")]
        [MaxLength(500, ErrorMessage = "Description cannot exceed 500 characters")]
        public string Description { get; set; }

        [Required(ErrorMessage = "Price is required")]
        [Range(0, 999999.99, ErrorMessage = "Price must be a positive number and cannot exceed 999999.99")]
        [DataType(DataType.Currency)]
        public decimal Price { get; set; }

        [Required(ErrorMessage = "Image is required")]
        public string ImagePath { get; set; }
    }
}
