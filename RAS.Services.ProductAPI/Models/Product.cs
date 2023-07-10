using System.ComponentModel.DataAnnotations;

namespace RAS.Services.ProductAPI.Models
{
    public class Product
    {
        [Key]
        public int ProductId { get; set; }

        [Required]
        public string Category { get; set; }

        [Required]
        public string Brand { get; set; }

        [Required]
        public string Model { get; set; }

        [Required,Range(1,999999)]
        public int Price { get; set; }

        [Required]
        public string ImageUrl { get; set; }

        public string Description { get; set; }
    }
}