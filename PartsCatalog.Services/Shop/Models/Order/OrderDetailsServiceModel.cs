namespace PartsCatalog.Services.Shop.Models.Order
{
    using System;
    using System.Collections.Generic;
    using AutoMapper;
    using Common.Mapping;
    using Data.Models;

    public class OrderDetailsServiceModel : IMapFrom<Order>, ICustomMapper
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

        public void ConfigureMapping(Profile profile)
        {

            profile.CreateMap<Order, OrderDetailsServiceModel>()
                .ForMember(o => o.Items, cfg => cfg
                    .MapFrom(o => o.Items))
                .ForMember(o => o.User, cfg => cfg
                .MapFrom(o => o.User));
        }
    }
}
