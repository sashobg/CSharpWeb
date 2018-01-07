using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AutoMapper.QueryableExtensions;
using PartsCatalog.Data;
using PartsCatalog.Data.Models;
using PartsCatalog.Services.Shop.Models.Category;

namespace PartsCatalog.Services.Shop.Implementations
{
    public class CategoryService : ICategoryService
    {
        private readonly PartsCatalogDbContext db;

        public CategoryService(PartsCatalogDbContext db)
        {
            this.db = db;
        }

        public IEnumerable<CategoryListingServiceModel> All()
            => this.db
                .Categories
                .OrderBy(x => x.Id)
                .ProjectTo<CategoryListingServiceModel>();

        public IEnumerable<SelectItemCategory> AllCategories()
            => this.db
                .Categories
                .OrderBy(x => x.Id)
                .ProjectTo<SelectItemCategory>();

        public CategoryDetailsServiceModel ById(int id)
            => this.db.Categories
                .Where(x => x.Id == id)
                .ProjectTo<CategoryDetailsServiceModel>()
                .FirstOrDefault();


        public string NameById(int? id)
        {
            if (id == null)
            {
                return null;
            }
            return this.db.Categories
                .Where(x => x.Id == id)
                .Select(c => c.Name)
                .FirstOrDefault();
        }

        public int Create(string title)
        {
            var category = new Category
            {
                Name = title
            };
            this.db.Categories.Add(category);
            this.db.SaveChanges();
            return category.Id;
        }

        public bool Update(int id, string title)
        {
            var category = this.db.Categories.Find(id);

            if (category == null)
            {
                return false;
            }

            category.Name = title;
            

            this.db.SaveChanges();
            return true;
        }

        public bool Delete(int id)
        {
            var category = this.db.Categories.Find(id);

            if (category == null)
            {
                return false;
            }

            this.db.Categories.Remove(category);
            this.db.SaveChanges();
            return true;
        }


    }
}
