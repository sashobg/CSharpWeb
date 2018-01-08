
namespace PartsCatalog.Areas.Admin.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Microsoft.AspNetCore.Mvc.Rendering;
    using Microsoft.AspNetCore.Mvc;
    using Infrastructure.Filters;
    using System.IO;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.AspNetCore.Identity;
    using Data.Models;
    using Services.Shop;
    using Models.Products;
    using Infrastructure.Extensions;

    public class ProductsController : BaseAdminController
    {
        private readonly UserManager<User> _userManager;
        private readonly IProductService _products;
        private readonly ICategoryService _categories;
        private readonly IHostingEnvironment _hostingEnvironment;
        public ProductsController(
            UserManager<User> userManager,
            IProductService products,
            IHostingEnvironment hostingEnvironment,
            ICategoryService categories)
        {
            this._userManager = userManager;
            this._products = products;
            this._hostingEnvironment = hostingEnvironment;
            this._categories = categories;
        }

        public IActionResult Create() => View(new AddProductFormModel()
        {
            Categories = this.GetSelectListCategories()
        });

        private List<SelectListItem> GetSelectListCategories()
        {
            return (from a in this._categories.AllCategories()
                select new SelectListItem
                {
                    Text = a.Name,
                    Value = a.Id.ToString()
                }).ToList();
        }
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




            var result = this._products.Create(model.Title, model.Price, model.Description, image, model.CategoryId);
            
            if (result == 0)
            {
                TempData.AddErrorMessage("Неуспешно добавен продукт.");

                return RedirectToAction("Index", "Home", new { area = "" });
            }

            TempData.AddSuccessMessage("Успешно добавен продукт.");

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
               CategoryId =  product.CategoryId,
               Categories = GetSelectListCategories()
               
            });

        }

        

        [HttpPost]
        [ValidateModelState]
        public IActionResult Edit(EditProductFormModel model)
        {
            if (this._categories.NameById(model.CategoryId) == null)
            {
                TempData.AddErrorMessage("Невалидна категория.");
                return RedirectToAction("Index", "Home", new { area = "" });

            }
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
            if (oldImagePath != "default.jpg")
            {
                this.DeleteOldImage(oldImagePath);
            }

            if (image)
            {
                var result = this._products.Update(model.Id, model.Title, model.Price, model.Description, uniqueFileName, model.CategoryId);
              
            }
            else
            {
                var result = this._products.Update(model.Id, model.Title, model.Price, model.Description, oldImagePath, model.CategoryId);

            }


            TempData.AddSuccessMessage("Успешно редактирахте продукта.");

            return RedirectToAction("Index", "Home", new { area = "" });
        }

        public IActionResult Delete(int id)
        {
            string oldImagePath = this._products.Details(id).Image;

            var result = this._products.Delete(id);
            if (result)
            {
                if (oldImagePath != "default.jpg")
                {
                    this.DeleteOldImage(oldImagePath);
                }

                TempData.AddSuccessMessage("Успешно изтрит продукт.");

                return RedirectToAction("Index", "Home", new { area = "" });


            }
            TempData.AddErrorMessage("Няма такъв продукт.");
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