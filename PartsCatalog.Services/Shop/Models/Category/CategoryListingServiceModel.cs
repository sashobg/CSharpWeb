
using AutoMapper;

namespace PartsCatalog.Services.Shop.Models.Category
{
    using PartsCatalog.Common.Mapping;
    using Data.Models;
    public class CategoryListingServiceModel : IMapFrom<Category>, ICustomMapper
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public void ConfigureMapping(Profile mapper)
        {
            mapper.CreateMap<Category, CategoryListingServiceModel>();
        }
    }
}
