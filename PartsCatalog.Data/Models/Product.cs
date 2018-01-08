namespace PartsCatalog.Data.Models
{
    using System.ComponentModel.DataAnnotations;
   
    using static DataConstants;

    public class Product
    {
        public int Id { get; set; }

        [Required]
        [MinLength(ProductTitleMinLength)]
        [MaxLength(ProductTitleMaxLength)]
        public string Title { get; set; }
        
        [Range(0, double.MaxValue)]
        public decimal Price { get; set; }

        public string Image { get; set; }

       
        [MaxLength(ProductDescriptionMaxLength)]
        public string Description { get; set; }
        
        public int CategoryId { get; set; }
        public Category Category { get; set; }
    }
}
