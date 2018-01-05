﻿
namespace PartsCatalog.Areas.Shop.Models
{
    public class CartItemViewModel
    {
        public int ProductId { get; set; }
        public string Title { get; set; }
        public decimal Price { get; set; }
        public int Quantity { get; set; }

        public string Image { get; set; }
    }
}
