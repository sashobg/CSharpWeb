using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using PartsCatalog.Areas.Shop.Models;
using PartsCatalog.Data.Models;
using PartsCatalog.Infrastructure.Extensions;
using PartsCatalog.Infrastructure.Filters;
using PartsCatalog.Services;
using PartsCatalog.Services.Models;
using PartsCatalog.Services.Shop;
using Microsoft.AspNetCore.Mvc.ViewFeatures;


namespace PartsCatalog.Areas.Shop.Controllers
{
    [Area("Shop")]

    public class OrdersController : Controller
    {
        private readonly IShoppingCartManager shoppingCartManager;
        private readonly UserManager<User> userManager;
        private readonly IOrderService orders;
        public OrdersController(
            IShoppingCartManager shoppingCartManager,
            UserManager<User> userManager,
            IOrderService orders)
        {
            this.shoppingCartManager = shoppingCartManager;
            this.userManager = userManager;
            this.orders = orders;
        }

        [Authorize]
        public IActionResult List()
        {
            var userId = this.userManager.GetUserId(User);

            var result = new OrderListingViewModel {Orders = this.orders.All(userId)};

            return View(result);
        }


        [Authorize]
        public IActionResult FinishOrder()
        {
            var shoppingCartId = this.HttpContext.Session.GetShoppingCartId();
            var items = this.shoppingCartManager.GetItems(shoppingCartId);
            if (items.Any())
            {
                return View();
            }
           
            return RedirectToAction("Items", nameof(ShoppingCart));
        }

        [HttpPost]
        [ValidateModelState]
        [Authorize]
        public IActionResult FinishOrder(OrderFormModel model)
        {
            var shoppingCartId = this.HttpContext.Session.GetShoppingCartId();
            var items = this.shoppingCartManager.GetItems(shoppingCartId);
            if (!items.Any())
            {
                return RedirectToAction("Items", nameof(ShoppingCart));

            }
            var userId = this.userManager.GetUserId(User);

            this.orders.Create(model.Address, model.Comment, items, userId);

            shoppingCartManager.Clear(shoppingCartId);
           TempData["SuccessMessage"] = "Успешна покупка";
            return RedirectToAction("Items", nameof(ShoppingCart));
        }
        [Authorize]
        public IActionResult Details(int id)
        {
            var userId = this.userManager.GetUserId(User);

            var result = this.orders.IsItAuthor(id, userId);
            if (result)
            {
                var order = this.orders.Details(id);
                return View(order);
            }

            return NotFound();
        }
    }

}