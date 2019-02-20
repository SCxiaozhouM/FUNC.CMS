using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;
using HS.Infrastructure;
using HS.Web.Areas.Admin.Data;
using Microsoft.AspNetCore.Mvc;

namespace HS.Web.Areas.Admin.Controllers
{
    [Area("Admin")]
    [DisplayName("菜单")]
    [Description("系统操作菜单以及功能目录树。支持排序，不可见菜单仅用于功能权限限制。每个菜单的权限子项由系统自动生成，请不要人为修改")]
    public class MenuController : BaseControllers
    {
        
        static MenuController()
        {
            MenuOrder = 11;
        }
        [EntityAuthorize(Infrastructure.PermissionFlags.Detail)]
        public IActionResult Index()
        {
            return View();
        }

        [DisplayName("测试")]
        [EntityAuthorize((Infrastructure.PermissionFlags)16)]
        public IActionResult Test()
        {
            return View();
        }
    }
}