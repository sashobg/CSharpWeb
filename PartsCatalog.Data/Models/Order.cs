namespace PartsCatalog.Data.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using static DataConstants;
    public class Order
    {
        public int Id { get; set; }


        public string UserId { get; set; }

        public User User { get; set; }

        public decimal TotalPrice { get; set; }

        [Required]
        [MinLength(OrderAddressMinLength)]
        [MaxLength(OrderAddressMaxLength)]
        public string Address { get; set; }

        [MaxLength(OrderCommentMaxLength)]
        public string Comment { get; set; }

        public DateTime Date { get; set; }

        public OrderStatus OrderStatus { get; set; }

        public List<OrderItem> Items { get; set; } = new List<OrderItem>();


    }
}
