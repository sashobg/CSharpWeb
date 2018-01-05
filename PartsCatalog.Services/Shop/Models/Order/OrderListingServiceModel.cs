
namespace PartsCatalog.Services.Shop.Models.Order
{
    using AutoMapper;
    using Common.Mapping;
    using Data.Models;

    
    public class OrderListingServiceModel : IMapFrom<Order>, ICustomMapper
    {

        public int Id { get; set; }

        public string UserId { get; set; }

        public User User { get; set; }

        public decimal TotalPrice { get; set; }

        public string Address { get; set; }

        public string Comment { get; set; }

        public void ConfigureMapping(Profile profile)
        {
            profile.CreateMap<Order, OrderListingServiceModel>()
                .ForMember(o => o.User, cfg => cfg
                .MapFrom(o => o.User));
        }
    }
}
