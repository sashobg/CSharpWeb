



namespace PartsCatalog.Services.Shop.Models
{

    using PartsCatalog.Data.Models;
    using AutoMapper;
    using Common.Mapping;
   


    public class ProductListingServiceModel : IMapFrom<Product>, ICustomMapper
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
            profile.CreateMap<Product, ProductListingServiceModel>()
                .ForMember(p => p.Category , cfg => cfg
                .MapFrom(p => p.Category));
        }
    }
}
