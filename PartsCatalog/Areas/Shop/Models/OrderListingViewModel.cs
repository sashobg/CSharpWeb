using System;
using System.Collections.Generic;
using System.Linq;
namespace PartsCatalog.Areas.Shop.Models
{
    using PartsCatalog.Services.Shop.Models.Order;

    public class OrderListingViewModel
    {
        public IEnumerable<OrderListingServiceModel> Orders { get; set; }

    }
}
