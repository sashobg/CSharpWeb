namespace PartsCatalog.Services.Shop
{
    using System.Collections.Generic;
    using Models.Order;
    using Models.Cart;
    public interface IOrderService
    {
        IEnumerable<OrderListingServiceModel> All(string searchText);

        OrderDetailsServiceModel Details(int id);

        int Create(string address, string comment, IEnumerable<CartItem> items, string userId);

        bool IsItAuthor(int id, string userId);

        bool UpdateStatus(int id, int status);

        bool Delete(int id);
    }
}
