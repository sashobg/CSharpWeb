
namespace PartsCatalog.Services.Shop.Models
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using AutoMapper;
    using PartsCatalog.Common.Mapping;
    using PartsCatalog.Data.Models;

    public class ProductListingServiceModel : IMapFrom<Product>, ICustomMapper
    {
        public int Id { get; set; }


        public string Title { get; set; }

        public decimal Price { get; set; }

        public string Image { get; set; }

        public string Description { get; set; }
        public void ConfigureMapping(Profile profile)
        {
            profile.CreateMap<Product, ProductDetailsServiceModel>();

        }
    }
}
