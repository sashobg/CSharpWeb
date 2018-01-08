namespace PartsCatalog.Areas.Admin.Controllers
{
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.Rendering;
    using Models.Orders;
    using Shop.Models;
    using Data.Models;
    using Infrastructure.Filters;
    using Services.Shop;
    using System;
    using System.Linq;
    using Infrastructure.Extensions;

    public class OrdersController : BaseAdminController
    {
        private readonly UserManager<User> _userManager;
        private readonly IOrderService _orders;
       
        public OrdersController(
            UserManager<User> userManager,
            IOrderService orders)
        {
            this._userManager = userManager;
            this._orders = orders;
        }

       
        public IActionResult ListAll()
        {
           

            var result = new OrderListingViewModel { Orders = this._orders.All("") };

            return View(result);
        }
        public IActionResult Details(int id)
        {
                var order = this._orders.Details(id);
            if (order != null)
            {
                var EnumList = Enum.GetValues(typeof(OrderStatus)).Cast<OrderStatus>().Select(v => new SelectListItem
                {
                    Text = v.ToString(),
                    Value = ((int)v).ToString()
                }).ToList();
                return View(new AdminOrderDetailsViewModel {Order = order, Status = EnumList, StatusId = (int)order.OrderStatus});
            }
            return RedirectToAction(nameof(ListAll));

        }

        [HttpPost]
        [ValidateModelState]
        public IActionResult UpdateStatus(AdminUpdateOrderStatusFormModel model)
        {
            var order = this._orders.Details(model.OrderId);
            if (order == null)
            {
                TempData.AddErrorMessage("Няма поръчка с Id: "+model.OrderId);
                return RedirectToAction(nameof(ListAll));

            }

           

            this._orders.UpdateStatus(order.Id, model.StatusId);
            TempData.AddSuccessMessage("Успешно обновихте статуса на проръчката.");

            return RedirectToAction("Index", "Home", new { area = "" });
        }

    }
}