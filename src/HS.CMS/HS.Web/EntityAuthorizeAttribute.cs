using HS.Infrastructure;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace HS.Web
{
    public class EntityAuthorizeAttribute : Attribute, IAuthorizationFilter
    {
        #region 属性
        /// <summary>授权项</summary>
        public PermissionFlags Permission { get; }

        /// <summary>是否全局特性</summary>
        internal Boolean IsGlobal;
        #endregion
        /// <summary>实例化实体授权特性</summary>
        public EntityAuthorizeAttribute() { }

        /// <summary>实例化实体授权特性</summary>
        /// <param name="permission"></param>
        public EntityAuthorizeAttribute(PermissionFlags permission)
        {
            if (permission <= PermissionFlags.None) throw new ArgumentNullException(nameof(permission));

            Permission = permission;
        }
        public void OnAuthorization(AuthorizationFilterContext filterContext)
        {
            /*
             * 验证范围：
             * 1，区域下的所有控制器
             * 2，所有带有EntityAuthorize特性的控制器或动作
             */
            var act = filterContext.ActionDescriptor;
            var ctrl = (ControllerActionDescriptor)act;

            // 允许匿名访问时，直接跳过检查
            if (
                ctrl.MethodInfo.IsDefined(typeof(AllowAnonymousAttribute)) ||
                ctrl.ControllerTypeInfo.IsDefined(typeof(AllowAnonymousAttribute))) return;

            // 如果控制器或者Action放有该特性，则跳过全局
            var hasAtt =
                ctrl.MethodInfo.IsDefined(typeof(EntityAuthorizeAttribute), true) ||
                ctrl.ControllerTypeInfo.IsDefined(typeof(EntityAuthorizeAttribute));

            if (IsGlobal && hasAtt) return;

            

            // 如果已经处理过，就不处理了
            if (filterContext.Result != null) return;

            if (!AuthorizeCore(filterContext.HttpContext))
            {
                HandleUnauthorizedRequest(filterContext);
            }
        }

        /// <summary>授权核心</summary>
        /// <param name="httpContext"></param>
        /// <returns></returns>
        protected Boolean AuthorizeCore(Microsoft.AspNetCore.Http.HttpContext httpContext)
        {


            // 判断当前登录用户
            var user = "";
            if (user == null) return false;

            // 判断权限
            return false;
        }

        /// <summary>未认证请求</summary>
        /// <param name="filterContext"></param>
        protected void HandleUnauthorizedRequest(AuthorizationFilterContext filterContext)
        {
            // 来到这里，有可能没登录，有可能没权限
            var prv = "";
            if (prv == null)
            {
                var retUrl = filterContext.HttpContext.Request.GetEncodedPathAndQuery();
                //.Host.ToString();//.Url?.PathAndQuery;

                var rurl = "~/Admin/User/Login";//.AppendReturn(retUrl);
                filterContext.Result = new RedirectResult(rurl);
            }
            else
            {
               // filterContext.Result = filterContext.NoPermission(Permission);
            }
        }
    }
}
