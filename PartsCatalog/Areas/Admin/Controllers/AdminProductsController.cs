


using System;
using PartsCatalog.Controllers;
using PartsCatalog.Services.Models;

namespace PartsCatalog.Areas.Admin.Controllers
{
  
    using Microsoft.AspNetCore.Mvc;
    using Models;
    using Infrastructure.Filters;
    using System.IO;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.AspNetCore.Identity;
    using Data.Models;
    using Services.Shop;


    public class AdminProductsController : BaseAdminController
    {
        private readonly UserManager<User> _userManager;
        private readonly IProductService _products;
        private IHostingEnvironment _hostingEnvironment;
        public AdminProductsController(
            UserManager<User> userManager,
            IProductService products,
            IHostingEnvironment hostingEnvironment)
        {
            this._userManager = userManager;
            this._products = products;
            this._hostingEnvironment = hostingEnvironment;
        }

        public IActionResult Create() => View();
        

        [HttpPost]
        [ValidateModelState]
        public IActionResult Create(AddProductFormModel model)
        {
            var uploads = Path.Combine(_hostingEnvironment.WebRootPath, "uploads");

            string image = "default.jpg";

            if (model.Image != null)
                if (model.Image.Length > 0)
                {


                    var uniqueFileName = this.GetUniqueName(model.Image.FileName);

                    var filePath = Path.Combine(uploads, uniqueFileName);
                    model.Image.CopyTo(new FileStream(filePath, FileMode.Create));

                     image = uniqueFileName;

                }




            var result = this._products.Create(model.Title, model.Price, model.Description, image);
            
            if (result == 0)
            {
                return RedirectToAction("Index", "Home", new { area = "" });
            }

            return RedirectToAction("Index", "Home", new { area = "" });
        }

        public IActionResult Edit(int id)
        {
            var product = this._products.Details(id); ;

            return View(new EditProductFormModel
            {
                Id = product.Id,
              Description  = product.Description,
              Title = product.Title,
              Price = product.Price,
               
            });

        }

        

        [HttpPost]
        [ValidateModelState]
        public IActionResult Edit(EditProductFormModel model)
        {
            var uploads = Path.Combine(_hostingEnvironment.WebRootPath, "uploads");
            string uniqueFileName="";
            bool image = false;
            if (model.Image != null)
                if (model.Image.Length > 0)
                {


                    uniqueFileName = this.GetUniqueName(model.Image.FileName);

                    var filePath = Path.Combine(uploads, uniqueFileName);
                    model.Image.CopyTo(new FileStream(filePath, FileMode.Create));

                    image = true;

                }


           string oldImagePath = this._products.Details(model.Id).Image;


            if (image)
            {
                var result = this._products.Update(model.Id, model.Title, model.Price, model.Description, uniqueFileName);
                this.DeleteOldImage(oldImagePath);
            }
            else
            {
                var result = this._products.Update(model.Id, model.Title, model.Price, model.Description, oldImagePath);

            }



            return RedirectToAction("Index", "Home", new { area = "" });
        }

        public IActionResult Delete(int id)
        {
            string oldImagePath = this._products.Details(id).Image;

            var result = this._products.Delete(id);
            if (result)
            {
                this.DeleteOldImage(oldImagePath);
                return RedirectToAction("Index", "Home", new { area = "" });


            }
            return RedirectToAction("Index", "Home", new { area = "" });

        }

        private void DeleteOldImage(string image)
        {
            var uploads = Path.Combine(_hostingEnvironment.WebRootPath, "uploads");

            System.IO.File.Delete(uploads + image);
        }

        private string GetUniqueName(string fileName)
        {
            fileName = Path.GetFileName(fileName);
            return Path.GetFileNameWithoutExtension(fileName)
                   + "_"
                   + Guid.NewGuid().ToString().Substring(0, 4)
                   + Path.GetExtension(fileName);
        }
    }
}