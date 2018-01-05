using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PartsCatalog.Services.Shop.Models.Order;

namespace PartsCatalog.Areas.Shop.Models
{
    public class OrderListingViewModel
    {
        public IEnumerable<OrderListingServiceModel> Orders { get; set; }

    }
}
