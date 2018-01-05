using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace PartsCatalog.Areas.Admin.Controllers
{
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;

    

    [Area("Admin")]
    [Authorize(Roles ="Administrator")]
    public abstract class BaseAdminController : Controller
    {
    }
}