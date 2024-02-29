using System.ComponentModel.DataAnnotations;

namespace Stach.API.DTOs
{
    public class BasketItemDTO
    {
        [Required]
        public int Id { get; set; }
        [Required]
        public string ProductName { get; set; }
        [Required]
        public string PictureUrl { get; set; }
        [Required]
        [Range(0.1, double.MaxValue, ErrorMessage = "Price must be greater than zero!")]
        public decimal Price { get; set; }
        [Required]
        public string Brand { get; set; }
        [Required]
        public string Category { get; set; }
        [Range(1, int.MaxValue, ErrorMessage = "Quantity must be at least one item!")]
        [Required]
        public int Quantity { get; set; }
    }
}