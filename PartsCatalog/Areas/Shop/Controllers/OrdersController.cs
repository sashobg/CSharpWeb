namespace PartsCatalog.Areas.Shop.Controllers
{

    using System.Linq;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using Models;
    using Data.Models;
    using Infrastructure.Extensions;
    using Infrastructure.Filters;
    using Services.Shop;
    using Services.Shop.Models.Cart;

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
                TempData.AddErrorMessage("За да направите покупка, първо добавете продукти в количката.");
                return RedirectToAction("Items", nameof(ShoppingCart));

            }
            var userId = this.userManager.GetUserId(User);

            this.orders.Create(model.Address, model.Comment, items, userId);

            shoppingCartManager.Clear(shoppingCartId);
            TempData.AddSuccessMessage("Успешна покупка.");

            return RedirectToAction("List");
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

        [Authorize]
        public IActionResult Cancel(int id)
        {
            var userId = this.userManager.GetUserId(User);

            var result = this.orders.IsItAuthor(id, userId);
            if (result)
            {
                var order = this.orders.Details(id);
                if (order.OrderStatus == OrderStatus.Pending)
                {
                    this.orders.UpdateStatus(order.Id, (int) OrderStatus.Cancelled);
                    TempData.AddSuccessMessage("Успешно отказахте поръчката.");


                    return RedirectToAction("List");

                }
                else
                {
                    TempData.AddErrorMessage("Поръчката не може да бъде отказана.");
                    return RedirectToAction("List");
                }
            }
            TempData.AddErrorMessage("Няма такава поръчка.");

            return RedirectToAction("List");
        }
    }

}