namespace PartsCatalog.Models
{
    using System.Collections.Generic;
    using Services.Shop.Models.Category;
    using Services.Shop.Models.Product;

    public class HomeProductsAndCategoryViewModel
    {
        public IEnumerable<ProductListingServiceModel> Products { get; set; }

        public IEnumerable<CategoryListingServiceModel> Categories { get; set; }
    }
}
