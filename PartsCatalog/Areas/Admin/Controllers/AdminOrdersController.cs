using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using PartsCatalog.Areas.Shop.Models;
using PartsCatalog.Data.Models;
using PartsCatalog.Services.Shop;

namespace PartsCatalog.Areas.Admin.Controllers
{
    public class AdminOrdersController : BaseAdminController
    {
        private readonly UserManager<User> _userManager;
        private readonly IOrderService _orders;
       
        public AdminOrdersController(
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
                return View(order);
            }
                return NotFound();
            
        }

    }
}