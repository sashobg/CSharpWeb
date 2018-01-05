using System;
using System.Collections.Generic;
using System.Text;

namespace PartsCatalog.Data.Models
{
    public class OrderItem
    {
        public int Id { get; set; }
        public int ProductId { get; set; }
        public string Title { get; set; }
        public decimal ProductPrice { get; set; }
        public int Quantity { get; set; }
        public int OrderId { get; set; }
        public Order Order { get; set; }
    }
}
