using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;
using HS.Web.Areas.Admin.Data;
using Microsoft.AspNetCore.Mvc;

namespace HS.Web.Areas.Admin.Controllers
{
    [Area("Admin")]
    [DisplayName("用户")]
    [Description("用户关系")]
    public class UserController : BaseControllers
    {
        static UserController()
        {
            MenuOrder = 1;
        }
        public IActionResult Index()
        {
            return View();
        }
    }
}