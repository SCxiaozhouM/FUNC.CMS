using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace HS.Web.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class MenuController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}