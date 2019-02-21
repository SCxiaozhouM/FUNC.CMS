using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;
using HS.Data;
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
        private ICommandInvokerFactory _commandInvokerFactory { get; set; }
        public int MenuOrder { get; set; } = 1;



        public UserController(ICommandInvokerFactory commandInvokerFactory)
        {
            this._commandInvokerFactory = commandInvokerFactory;
        }

        /// <summary>
        /// 后天用户登录
        /// </summary>
        /// <returns></returns>
        //[HttpPost]
        public IActionResult Login(UserLoginCommand command)
        {
            //验证
            if (!ModelState.IsValid)
            {
                return Json(new ResultJson { State = ResultState.Error, Msg = "账号或密码错误!" });
            }
            var commandResult = this._commandInvokerFactory.Handle<UserLoginCommand, UserLoginCommandResult>(command);
            //是否执行成功
            if (!commandResult.IsSuccess)
            {
                return Json(new ResultJson { State = ResultState.Error, Msg = commandResult.GetErrors()[0] });
            }

            return Json(new ResultJson { State=ResultState.OK,Msg="登录成功!",Data=new { url="/admin"} });
        }
    }
}