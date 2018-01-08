namespace PartsCatalog.Areas.Admin.Models.Orders
{
    using System.ComponentModel.DataAnnotations;
    public class AdminUpdateOrderStatusFormModel
    {
            [Required]
            public int OrderId { get; set; }

            [Required]
            public int StatusId { get; set; }

    }
}
