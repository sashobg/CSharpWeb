namespace PartsCatalog.Services.Shop
{
    using System.Collections.Generic;
    using Models.Category;

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
