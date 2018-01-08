namespace PartsCatalog.Areas.Shop.Controllers
{
    using System.Linq;
    using Microsoft.AspNetCore.Mvc;
    using Infrastructure.Extensions;
    using Microsoft.AspNetCore.Identity;
    using Models;
    using Data;
    using Data.Models;
    using Services.Shop;

    [Area("Shop")]
    public class ShoppingCartController : Controller
    {
        private readonly IShoppingCartManager shoppingCartManager;
        private readonly UserManager<User> userManager;
        private readonly IProductService products;
        private readonly PartsCatalogDbContext db;
        public ShoppingCartController(IShoppingCartManager shoppingCartManager, 
            UserManager<User> userManager,
            IProductService products,
            PartsCatalogDbContext db)
        {
            this.shoppingCartManager = shoppingCartManager;
            this.userManager = userManager ;
            this.products = products;
            this.db = db;
        }
      

        public IActionResult Items()
        {
            var shoppingCartId = this.HttpContext.Session.GetShoppingCartId();

            var items = this.shoppingCartManager.GetItems(shoppingCartId);
            var itemIds = items.Select(i => i.ProductId);
            var itemQuatities = items.ToDictionary(i => i.ProductId, i => i.Quantity);

            var itemsWithDetails = this.products.ByIds(itemIds)
                .Select(pr => new CartItemViewModel
                {
                    ProductId = pr.Id,
                    Title = pr.Title,
                    Price = pr.Price,
                    Image = pr.Image
                })
                .ToList();

            itemsWithDetails.ForEach(i => i.Quantity = itemQuatities[i.ProductId]);
            return View(itemsWithDetails);
        }

        public IActionResult AddToCart(int id)
        {
            var shoppingCartId = this.HttpContext.Session.GetShoppingCartId();

           
            this.shoppingCartManager.AddToCart(shoppingCartId, id, 1); // set quantity 1 
            TempData.AddSuccessMessage("Добавихте продукт в количката.");

            return RedirectToAction(nameof(Items));
        }
        public IActionResult RemoveItem(int id)
        {
            var shoppingCartId = this.HttpContext.Session.GetShoppingCartId();
            this.shoppingCartManager.RemoveFromCart(shoppingCartId,id);

            return RedirectToAction(nameof(Items));
        }

      
      
    }
}