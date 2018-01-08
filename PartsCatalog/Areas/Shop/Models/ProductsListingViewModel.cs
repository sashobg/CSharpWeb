namespace PartsCatalog.Areas.Shop.Models
{
    using System;
    using System.Collections.Generic;
    using Services;
    using Services.Shop.Models.Product;

    public class ProductsListingViewModel
    {
        
            public IEnumerable<ProductListingServiceModel> Products { get; set; }

            public int TotalProducts { get; set; }

            public int TotalPages => (int)Math.Ceiling((double)this.TotalProducts / ServiceConstants.ShopProductsPageSize);

            public int CurrentPage { get; set; }

            public int PreviousPage => this.CurrentPage <= 1 ? 1 : this.CurrentPage - 1;

            public int NextPage
                => this.CurrentPage == this.TotalPages
                    ? this.TotalPages
                    : this.CurrentPage + 1;
        
    }
}
