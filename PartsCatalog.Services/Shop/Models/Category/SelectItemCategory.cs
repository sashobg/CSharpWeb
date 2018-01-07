namespace PartsCatalog.Services.Shop.Models.Category
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using PartsCatalog.Common.Mapping;
    using Data.Models;
    public class SelectItemCategory : IMapFrom<Category>
    {
            public int Id { get; set; }
            public string Name { get; set; }
       
    }
}
