using System.Collections.Generic;
using PartsCatalog.Data.Models;
using PartsCatalog.Services.Models;
using PartsCatalog.Services.Shop.Models;
using PartsCatalog.Services.Shop.Models.Cart;

namespace PartsCatalog.Services.Shop
{
    public interface IProductService
    {

        IEnumerable<Product> ByIds(IEnumerable<int> ids);

        IEnumerable<ProductListingServiceModel> All(string searchText, int page);

        ProductDetailsServiceModel Details(int id);

        int Create(string title, decimal price, string description, string image);

        bool Exists(int id);
        int Total();
        bool Update(int id, string title, decimal price, string description, string image);

        List<CartItemWithDetails> GetProducts(IEnumerable<CartItem> items);

        bool Delete(int id);
    }
}
