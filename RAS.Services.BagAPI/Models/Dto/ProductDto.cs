using System.ComponentModel.DataAnnotations;

namespace RAS.Services.BagAPI.Models.Dto
{
    public class ProductDto
    {
        public int ProductId { get; set; }
        public string Category { get; set; }
        public string Brand { get; set; }
        public string Model { get; set; }
        public int Price { get; set; }
        public string ImageUrl { get; set; }
        public string Description { get; set; }
    }
}