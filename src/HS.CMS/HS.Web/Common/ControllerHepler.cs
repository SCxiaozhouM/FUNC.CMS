using HS.Infrastructure;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using System;
using System.Collections.Generic;
using System.Text;

namespace HS.Web.Common
{
    public static class ControllerHepler
    {
        /// <summary>无权访问</summary>
        /// <param name="filterContext"></param>
        /// <param name="pm"></param>
        /// <returns></returns>
        public static ActionResult NoPermission(this AuthorizationFilterContext filterContext, PermissionFlags pm)
        {
            var act = (ControllerActionDescriptor)filterContext.ActionDescriptor;
            var ctrl = act;

            var ctx = filterContext.HttpContext;

            var res = string.Format( "[{0}/{1}]",ctrl.ControllerName, act.ActionName);
            var msg = string.Format("访问资源 {0} 需要权限",res);
            //LogProvider.Provider.WriteLog("访问", "拒绝", msg, ip: ctx.Request.GetUserHost());

            //var menu = ctx.Items["CurrentMenu"] as IMenu;

            var vr = new ViewResult()
            {
                ViewName = "NoPermission"
            };

            //vr.Context = filterContext;//不需要赋值Context，执行的时候会自己获取Context
            vr.ViewData =
                new ViewDataDictionary(new EmptyModelMetadataProvider(),
                    filterContext.ModelState)
                {
                    ["Resource"] = res,
                    ["Permission"] = pm
                };

            return vr;
        }
    }
}
