namespace PartsCatalog.Areas.Shop.Models
{
    using System.ComponentModel.DataAnnotations;

    public class OrderFormModel
    {
        [Required]
        public string Address { get; set; }
        public string Comment { get; set; }

    }
}
