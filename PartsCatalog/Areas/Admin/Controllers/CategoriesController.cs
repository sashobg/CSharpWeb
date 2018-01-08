namespace PartsCatalog.Areas.Admin.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using Services.Shop;
    using Models.Categories;
    using Infrastructure.Extensions;

    public class CategoriesController : BaseAdminController
    {
        private readonly ICategoryService _categoryService;
        private readonly IProductService _productService;

        public CategoriesController(ICategoryService categoryService, IProductService productService)
        {
            this._categoryService = categoryService;
            this._productService = productService;
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
                TempData.AddErrorMessage("Моля, проверете въведените данни и опитайте отново.");

                return View(model);
            }

            this._categoryService.Create(model.Name);

            TempData.AddSuccessMessage("Успешно добавихте категория.");
            return RedirectToAction(nameof(List));
        }


        public IActionResult Edit(int Id)
        {
            var category = this._categoryService.ById(Id);
            if (category == null)
            {
                TempData.AddErrorMessage("Моля, проверете въведените данни и опитайте отново.");
                return RedirectToAction(nameof(List));

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
                TempData.AddErrorMessage("Моля, проверете въведените данни и опитайте отново.");

                return View(model);
            }

            this._categoryService.Update(Id,
                model.Name);

            TempData.AddSuccessMessage("Успешно редактирахте категория.");

            return RedirectToAction(nameof(List));
        }

        
        public IActionResult Delete(int Id)
        {
            if (this._categoryService.HasProducts(Id))
            {
                TempData.AddErrorMessage("Категорията не може да бъде изтрита, има продукти от този тип.");
                return RedirectToAction(nameof(List));

            }
            this._categoryService.Delete(Id);
            TempData.AddSuccessMessage("Успешно изтрихте категория.");

            return RedirectToAction(nameof(List));
        }
    }
}