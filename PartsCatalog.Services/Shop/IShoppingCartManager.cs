namespace PartsCatalog.Services.Shop
{
    using System.Collections.Generic;
    using Models.Cart;

    public interface IShoppingCartManager
    {
        void AddToCart(string id, int productId, int quantity);

        void RemoveFromCart(string id, int productId);

        IEnumerable<CartItem> GetItems(string id);
        void Clear(string shoppingCartId);
    }
}
