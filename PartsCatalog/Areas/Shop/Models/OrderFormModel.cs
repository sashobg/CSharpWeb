using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace PartsCatalog.Areas.Shop.Models
{
    public class OrderFormModel
    {
        [Required]
        public string Address { get; set; }
        public string Comment { get; set; }

    }
}
