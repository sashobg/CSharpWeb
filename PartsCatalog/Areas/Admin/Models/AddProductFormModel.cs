using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace PartsCatalog.Areas.Admin.Models
{
    public class AddProductFormModel 
    {
        [Required]
        public string Title { get; set; }

        [Range(0, double.MaxValue)]
        public decimal Price { get; set; }
       
        public string Description { get; set; }

        public IFormFile Image { get; set; }
    }

}
