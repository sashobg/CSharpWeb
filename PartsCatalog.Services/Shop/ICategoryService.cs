using System.Collections.Generic;
using PartsCatalog.Services.Shop.Models.Category;

namespace PartsCatalog.Services.Shop
{
    public interface ICategoryService
    {
        IEnumerable<CategoryListingServiceModel> All();


        IEnumerable<SelectItemCategory> AllCategories();

        CategoryDetailsServiceModel ById(int id);
        string NameById(int? id);

        int Create(string title);
       
        bool Update(int id, string title);

        bool Delete(int id);

    }
}
