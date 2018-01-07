using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PartsCatalog.Services.Shop.Models;
using PartsCatalog.Services.Shop.Models.Category;

namespace PartsCatalog.Models
{
    public class HomeProductsAndCategoryViewModel
    {
        public IEnumerable<ProductListingServiceModel> Products { get; set; }

        public IEnumerable<CategoryListingServiceModel> Categories { get; set; }
    }
}
