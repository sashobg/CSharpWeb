namespace PartsCatalog.Services.Shop.Models.Category
{
    using Common.Mapping;
    using Data.Models;
    using AutoMapper;

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
