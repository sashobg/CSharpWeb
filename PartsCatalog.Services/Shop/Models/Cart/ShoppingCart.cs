using System.Linq;

namespace PartsCatalog.Services.Models
{
    using System.Collections.Generic;

    public class ShoppingCart
    {
        private readonly IList<CartItem> items;

        public ShoppingCart()
        {
            this.items = new List<CartItem>();
        }

        public void AddToCart(int productId, int quantity)
        {
            var cartItem = this.Items.FirstOrDefault(p => p.ProductId == productId);

            if (cartItem == null) 
            {
                cartItem = new CartItem
                {
                    ProductId = productId,
                    Quantity = quantity
                };
                this.items.Add(cartItem);
            }
            else
            {
                cartItem.Quantity++;
            }
            
        }

        public void RemoveFromCart(int productId)
        {
            var cartItem = this.items.FirstOrDefault(i => i.ProductId == productId);
            if (cartItem != null)
            {
                this.items.Remove(cartItem);
            }

            
        }

        public void Clear()
        {
            this.items.Clear();
        }
        public IEnumerable<CartItem> Items => new List<CartItem>(this.items);
    }
}
