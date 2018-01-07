namespace PartsCatalog.Services.Shop.Models
{
    using AutoMapper;
    using Common.Mapping;
    using Data.Models;

    public class ProductDetailsServiceModel : IMapFrom<Product>, ICustomMapper
    {
        public int Id { get; set; }

       
        public string Title { get; set; }
       
        public decimal Price { get; set; }

        public string Image { get; set; }

        public string Description { get; set; }

        public int CategoryId { get; set; }
        public Data.Models.Category Category { get; set; }
        public void ConfigureMapping(Profile profile)
        {
            profile.CreateMap<Product, ProductDetailsServiceModel>()
                .ForMember(p => p.Category, cfg => cfg
                    .MapFrom(p => p.Category));
        }
    }
}
