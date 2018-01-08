namespace PartsCatalog.Areas.Admin.Models.Orders
{
    using System.Collections.Generic;
    using Microsoft.AspNetCore.Mvc.Rendering;
    using Services.Shop.Models.Order;
    public class AdminOrderDetailsViewModel
    {
        public OrderDetailsServiceModel Order { get; set; }
        public int StatusId { get; set; }

        public List<SelectListItem> Status { get; set; }

    }
}
