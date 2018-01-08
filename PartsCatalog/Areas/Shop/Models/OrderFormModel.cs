namespace PartsCatalog.Areas.Shop.Models
{
    using System.ComponentModel.DataAnnotations;
    using static Data.DataConstants;
    public class OrderFormModel
    {
        [Required]
        [MinLength(OrderAddressMinLength)]
        [MaxLength(OrderAddressMaxLength)]
        [Display(Name = "Адрес")]
        public string Address { get; set; }

        [Display(Name = "Коментар (телефон за връзка)")]
        [MaxLength(OrderCommentMaxLength)]
        public string Comment { get; set; }

    }
}
