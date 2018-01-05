using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using PartsCatalog.Areas.Shop.Models;
using PartsCatalog.Data.Models;
using PartsCatalog.Services.Shop;

namespace PartsCatalog.Areas.Shop.Controllers
{
    [Area("Shop")]
    public class ProductsController : Controller
    {
        private readonly UserManager<User> _userManager;
        private readonly IProductService _products;
      
        public ProductsController(
            UserManager<User> userManager,
            IProductService products
           )
        {
            this._userManager = userManager;
            this._products = products;
          
        }

        public IActionResult Details(int id)
        {
            
            var product = this._products.Details(id);
            if (product != null)
            {
                return View(product);
            }

            return NotFound();
        }

        public IActionResult List(string search, int page = 1)
        {
            var result = new ProductsListingViewModel
            {
                Products = this._products.All(search, page),
                TotalProducts = this._products.Total(),
                CurrentPage = page
            };
            return View(result);
        }
    }
}