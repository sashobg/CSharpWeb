namespace PartsCatalog.Areas.Admin.Models.Products
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc.Rendering;

    using static Data.DataConstants;

    public class EditProductFormModel
    {
        public int Id { get; set; }

     
        [Required]
        [MinLength(ProductTitleMinLength)]
        [MaxLength(ProductTitleMaxLength)]
        [Display(Name = "Заглавие")]
        public string Title { get; set; }

        [Range(0, double.MaxValue)]
        [Display(Name = "Цена")]
        public decimal Price { get; set; }

        [Display(Name = "Описание")]
        [MaxLength(ProductDescriptionMaxLength)]
        public string Description { get; set; }

        [Display(Name = "Снимка")]
        public IFormFile Image { get; set; }

        [Required]
        public int CategoryId { get; set; }

        [Display(Name = "Категория")]
        public List<SelectListItem> Categories { get; set; }
   }
}
