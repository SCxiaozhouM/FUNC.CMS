using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HS.Web.Areas.Admin.Data
{
    public class BaseControllers : Controller
    {
        public BaseControllers()
        {
        }
        /// <summary>菜单顺序。扫描是会反射读取</summary>
        protected static Int32 MenuOrder { get; set; } = 10;
    }
}
