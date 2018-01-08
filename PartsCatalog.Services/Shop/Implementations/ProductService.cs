namespace PartsCatalog.Services.Shop.Implementations
{
    using System.Collections.Generic;
    using Models;
    using Data.Models;
    using Data;
    using System.Linq;
    using AutoMapper.QueryableExtensions;
    using Models.Product;
    using Models.Cart;

    using static ServiceConstants;

    public class ProductService : IProductService
    {
        private readonly PartsCatalogDbContext db;
        public ProductService(PartsCatalogDbContext db)
        {
            this.db = db;
        }

        public IEnumerable<Product> ByIds(IEnumerable<int> ids)
        {
            return this.db.Products
                .Where(pr => ids.Contains(pr.Id))
                .ToList();
        }



        public IEnumerable<ProductListingServiceModel> All(int? categoryId,string searchText, int page = 1)
        {
            
            if (searchText == null)
                searchText = string.Empty;

            if (categoryId != null)
            {
                return this.db
                    .Products
                    .Where(x=>x.CategoryId == categoryId)
                    .Where(x => x.Title.ToLower()
                        .Contains(searchText.ToLower()))
                        
                    .OrderBy(x => x.Title)
                  
                    .ProjectTo<ProductListingServiceModel>()
                    .ToList();
            }

            return this.db
                .Products
                .Where(x => x.Title.ToLower()
                .Contains(searchText.ToLower()))
                .OrderBy(x => x.Title)
                .ProjectTo<ProductListingServiceModel>()
                .ToList();
        }

        public IEnumerable<ProductListingServiceModel> Recent()
        {
            return this.db
                .Products
                .OrderByDescending(x => x.Id)
                .Take(6)
                .ProjectTo<ProductListingServiceModel>()
                .ToList();
        }

        public int Total()
            =>  this.db.Products.Count();

        public ProductDetailsServiceModel Details(int id)
            => this.db.Products
                .Where(x => x.Id == id)
                .ProjectTo<ProductDetailsServiceModel>()
                .FirstOrDefault();

        public int Create(string title, decimal price, string description, string image, int categoryId)
        {
            var product = new Product
            {
                Description = description,
                Price = price,
                Title = title,
                Image = image,
                CategoryId = categoryId
                
            };
            this.db.Products.Add(product);
            this.db.SaveChanges();
            return product.Id;
        }

        public bool Exists(int id) => this.db.Products.Any(p => p.Id == id);
        

        public bool Update(int id, string title, decimal price, string description, string image, int categoryId)
        {
            var product = db.Products.FirstOrDefault(p => p.Id == id);
            if (product == null)
            {
                return false;
            }


            product.Title = title;
            product.Price = price;
            product.Description = description;
            product.Image = image;
            product.CategoryId = categoryId;
            this.db.Products.Update(product);
            this.db.SaveChanges();
            return true;
        }

        public bool Delete(int id)
        {
            var product = this.db.Products.Find(id);

            if (product == null)
            {
                return false;
            }

            this.db.Products.Remove(product);
            this.db.SaveChanges();
            return true;

        }

        public List<CartItemWithDetails> GetProducts(IEnumerable<CartItem> items)
        {
            var itemIds = items.Select(i => i.ProductId);

            var itemsWithDetails = this.db
                .Products
                .Where(pr => itemIds.Contains(pr.Id))
                .Select(pr => new CartItemWithDetails
                {
                    ProductId = pr.Id,
                    Title = pr.Title,
                    Price = pr.Price
                })
                .ToList();

            var itemQuatities = items.ToDictionary(i => i.ProductId, i => i.Quantity);
            itemsWithDetails.ForEach(i => i.Quantity = itemQuatities[i.ProductId]);
            return itemsWithDetails;
        }
    }
}
