using HS.Data.Command.Account;
using HS.Data.Configuration;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace HS.Web.Common
{
    public class AccountLoginManager
    {
        /// <summary>
        /// 设置登录标记
        /// </summary>
        /// <param name="commandResult"></param>
        public static void SetLogin(HttpContext httpContext, UserLoginCommandResult commandResult)
        {
            httpContext.Session.SetString(AppConsts._session_server, commandResult.UserInfo.Account);
            // 指定身份认证类型
            var identity = new ClaimsIdentity("Forms");
            // 用户名称
            var tempC = new Claim(ClaimTypes.Sid, commandResult.UserInfo.Account);
            identity.AddClaim(tempC);
            var principal = new ClaimsPrincipal(identity);
            // 登陆
            httpContext.SignInAsync(AppConsts._auth, principal, new AuthenticationProperties
            {
                IsPersistent = true
            });
        }


        /// <summary>
        /// 清除登陆痕迹
        /// </summary>
        public static void SetLoginOut(HttpContext httpContext)
        {
            httpContext.Session.Remove(AppConsts._session_server);
            httpContext.SignOutAsync(AppConsts._auth);
        }
    }
}
