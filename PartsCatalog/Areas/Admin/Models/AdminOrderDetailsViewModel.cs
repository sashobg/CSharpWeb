using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Rendering;
using PartsCatalog.Services.Shop.Models.Order;

namespace PartsCatalog.Areas.Admin.Models
{
    public class AdminOrderDetailsViewModel
    {
        public OrderDetailsServiceModel Order { get; set; }
        public int StatusId { get; set; }

        public List<SelectListItem> Status { get; set; }

    }
}
