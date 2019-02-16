using HS.Infrastructure;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
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
        public void OnAuthorization(AuthorizationFilterContext context)
        {
            throw new NotImplementedException();
        }
    }
}
