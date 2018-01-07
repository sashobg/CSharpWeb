using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using PartsCatalog.Areas.Admin.Models;
using PartsCatalog.Services.Shop;

namespace PartsCatalog.Areas.Admin.Controllers
{
    public class CategoryController : BaseAdminController
    {
        private readonly ICategoryService _categoryService;

        public CategoryController(ICategoryService categoryService)
        {
            this._categoryService = categoryService;
        }
        

        public IActionResult List()
            => View(this._categoryService.All());

        public IActionResult Create()
            => View();

        [HttpPost]
        public IActionResult Create(CategoryFormModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            this._categoryService.Create(model.Name);


            return RedirectToAction(nameof(List));
        }


        public IActionResult Edit(int Id)
        {
            var category = this._categoryService.ById(Id);
            if (category == null)
            {
                return NotFound();
            }

            return View(new CategoryFormModel()
            {
                Name = category.Name
               
            });
        }

        [HttpPost]
        public IActionResult Edit(int Id, CategoryFormModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            this._categoryService.Update(Id,
                model.Name);

            return RedirectToAction(nameof(List));
        }

        
        public IActionResult Delete(int Id)
        {
            this._categoryService.Delete(Id);
            return RedirectToAction(nameof(List));
        }
    }
}