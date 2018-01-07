using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PartsCatalog.Areas.Admin.Models
{
    public class CategoryListingViewModel
    {

        public IEnumerable<CategoryListingViewModel> Categories { get; set; }
    }
}
