using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace HS.Infrastructure
{
    /// <summary>
    /// 权限枚举
    /// </summary>
    public enum PermissionFlags
    {

        /// <summary>
        ///  无权限
        /// </summary>
        [Description("无权限")]
        None = 0,
        /// <summary>
        ///  查看权限
        /// </summary>
        [Description("查看")]
        Detail = 1,
        /// <summary>
        ///   添加权限
        /// </summary>
        [Description("添加")]
        Insert = 2,
        //
        // 摘要:
        //     修改权限
        [Description("修改")]
        Update = 4,
        //
        // 摘要:
        //     删除权限
        [Description("删除")]
        Delete = 8,
        //
        // 摘要:
        //     所有权限
        [Description("所有权限")]
        All = 255
    }
}
