﻿namespace PartsCatalog.Data.Models
{
    using System.ComponentModel.DataAnnotations;

    public class Product
    {
        public int Id { get; set; }

        [Required]
        public string Title { get; set; }
        
        [Range(0, double.MaxValue)]
        public decimal Price { get; set; }

        public string Image { get; set; }

        public string Description { get; set; }
    }
}
