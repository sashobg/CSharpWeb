

namespace PartsCatalog.Areas.Shop.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc;
    using Services;
    using Infrastructure.Extensions;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Identity;
    using PartsCatalog.Areas.Shop.Models;
    using PartsCatalog.Data;
    using PartsCatalog.Data.Models;
    using PartsCatalog.Services.Models;
    using PartsCatalog.Services.Shop;
    using PartsCatalog.Services.Shop.Models.Cart;

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