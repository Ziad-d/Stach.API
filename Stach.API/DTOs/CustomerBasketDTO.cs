using Stach.Domain.Models;
using System.ComponentModel.DataAnnotations;

namespace Stach.API.DTOs
{
    public class CustomerBasketDTO
    {
        [Required]
        public string Id { get; set; }
        public List<BasketItemDTO> Items { get; set; }
    }
}
