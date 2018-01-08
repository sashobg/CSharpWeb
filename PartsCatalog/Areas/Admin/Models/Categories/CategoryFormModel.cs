
namespace PartsCatalog.Areas.Admin.Models.Categories
{
    using System.ComponentModel.DataAnnotations;
    using static Data.DataConstants;

    public class CategoryFormModel
    {
        [Display(Name = "Номер")]
        public int Id { get; set; }

        [Required]
        [MinLength(CategoryNameMinLength)]
        [MaxLength(CategoryNameMaxLength)]
        [Display(Name = "Име")]
        public string Name { get; set; }
    }
}
