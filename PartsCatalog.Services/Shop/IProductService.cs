namespace PartsCatalog.Services.Shop
{
    using System.Collections.Generic;
    using Data.Models;
    using Models.Cart;
    using Models.Product;
    public interface IProductService
    {

        IEnumerable<Product> ByIds(IEnumerable<int> ids);

        IEnumerable<ProductListingServiceModel> All(int? categoryId, string searchText, int page);

        IEnumerable<ProductListingServiceModel> Recent();


        ProductDetailsServiceModel Details(int id);

        int Create(string title, decimal price, string description, string image, int categoryId);

        bool Exists(int id);
        int Total();
        bool Update(int id, string title, decimal price, string description, string image, int categoryId);

        List<CartItemWithDetails> GetProducts(IEnumerable<CartItem> items);

        bool Delete(int id);
    }
}
