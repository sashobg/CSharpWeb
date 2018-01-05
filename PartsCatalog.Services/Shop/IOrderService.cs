using System.Collections.Generic;
using PartsCatalog.Services.Models;
using PartsCatalog.Services.Shop.Models;
using PartsCatalog.Services.Shop.Models.Order;

namespace PartsCatalog.Services.Shop
{
    public interface IOrderService
    {
        IEnumerable<OrderListingServiceModel> All(string searchText);

        OrderDetailsServiceModel Details(int id);

        int Create(string address, string comment, IEnumerable<CartItem> items, string userId);

        bool IsItAuthor(int id, string userId);

        bool Delete(int id);
    }
}
