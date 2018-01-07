using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using PartsCatalog.Data;
using PartsCatalog.Models;
using PartsCatalog.Services.Shop;

namespace PartsCatalog.Controllers
{
    public class HomeController : Controller
    {
        private readonly IProductService _products;
        private readonly ICategoryService _categories;
        public HomeController(IProductService products, ICategoryService _categories)
        {
            this._products = products;
            this._categories = _categories;
        }
        public IActionResult Index()
        {
            var products = this._products.Recent();
            var categories = this._categories.All();
            return View(new HomeProductsAndCategoryViewModel{Categories = categories, Products = products});
        }

        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";

            return View();
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }

        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
