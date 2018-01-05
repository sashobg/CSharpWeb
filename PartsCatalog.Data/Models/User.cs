namespace PartsCatalog.Data.Models
{
    using Microsoft.AspNetCore.Identity;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    public class User : IdentityUser
    {
        public List<Order> Orders { get; set; } = new List<Order>();



    }
}
