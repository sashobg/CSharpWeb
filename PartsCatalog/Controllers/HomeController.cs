using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using PartsCatalog.Data;
using PartsCatalog.Models;

namespace PartsCatalog.Controllers
{
    public class HomeController : Controller
    {
        private readonly PartsCatalogDbContext db;

        public HomeController(PartsCatalogDbContext db)
        {
            this.db = db;
        }
        public IActionResult Index()
        {
            var products = db.Products.ToList();
            return View(products);
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
