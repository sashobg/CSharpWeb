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
        private readonly ICategoryService _categories;
        public ProductsController(
            UserManager<User> userManager,
            IProductService products,
       ICategoryService categories

           )
        {
            this._userManager = userManager;
            this._products = products;
            this._categories = categories;

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

        public IActionResult List(int? categoryId, string search, int page = 1)
        {
            var result = new ProductsListingViewModel
            {
                Products = this._products.All(categoryId,search, page),
                TotalProducts = this._products.Total(),
                CurrentPage = page
            };

            @ViewBag.category = this._categories.NameById(categoryId);
            @ViewBag.query = search;
            return View(result);
        }
    }
}