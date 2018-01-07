using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace PartsCatalog.Areas.Admin.Models
{
    public class AdminUpdateOrderStatusFormModel
    {

        
            [Required]
            public int OrderId { get; set; }

            [Required]
            public int StatusId { get; set; }
        

    }
}
