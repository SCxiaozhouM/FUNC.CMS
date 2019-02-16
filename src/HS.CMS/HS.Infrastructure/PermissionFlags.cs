using System;
using System.Collections.Generic;
using System.Text;

namespace HS.Infrastructure
{
    /// <summary>
    /// 权限枚举
    /// </summary>
    public enum PermissionFlags
    {
        //
        // 摘要:
        //     无权限
        None = 0,
        //
        // 摘要:
        //     查看权限
        Detail = 1,
        //
        // 摘要:
        //     添加权限
        Insert = 2,
        //
        // 摘要:
        //     修改权限
        Update = 4,
        //
        // 摘要:
        //     删除权限
        Delete = 8,
        //
        // 摘要:
        //     所有权限
        All = 255
    }
}
