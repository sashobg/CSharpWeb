using System;
using System.Collections.Generic;
using System.Text;

namespace PartsCatalog.Data.Models
{
    public class Order
    {
        public int Id { get; set; }


        public string UserId { get; set; }

        public User User { get; set; }

        public decimal TotalPrice { get; set; }

        public string Address { get; set; }

        public string Comment { get; set; }

        public DateTime Date { get; set; }

        public OrderStatus OrderStatus { get; set; }

        public List<OrderItem> Items { get; set; } = new List<OrderItem>();


    }
}
