using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;
using HS.Data.Command.Account;
using HS.Infrastructure.Command;
using HS.Web.Areas.Admin.Data;
using HS.Web.Common;
using Microsoft.AspNetCore.Mvc;

namespace HS.Web.Areas.Admin.Controllers
{
    [Area("Admin")]
    [DisplayName("用户")]
    [Description("用户操作")]
    public class UserController : BaseControllers
    {
        static UserController()
        {
            MenuOrder = 1;
        }
        private readonly ICommandInvokerFactory _commandInvokerFactory;



        public UserController(ICommandInvokerFactory commandInvokerFactory)
        {
            this._commandInvokerFactory = commandInvokerFactory;

        }

        /// <summary>
        /// 后天用户登录
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public IActionResult Login(UserLoginCommand command)
        {
            if(!ModelState.IsValid)
            {
                return Json(new ResultJson());
            }
            return Json(new ResultJson());

        }
    }
}