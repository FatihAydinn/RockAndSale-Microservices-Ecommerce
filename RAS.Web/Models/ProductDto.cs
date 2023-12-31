﻿using System.ComponentModel.DataAnnotations;

namespace RAS.Web.Models
{
    public class ProductDto
    {
        public ProductDto()
        {
            Count = 1;
        }

        public int ProductId { get; set; }
        public string Brand { get; set; }
        public string GuitarModel { get; set; }
        public int Price { get; set; }
        public string Description { get; set; }
        public string ImageUrl { get; set; }
        public string Category { get; set; }

        [Range(1, 100)]
        public int Count { get; set; }
    }
}
