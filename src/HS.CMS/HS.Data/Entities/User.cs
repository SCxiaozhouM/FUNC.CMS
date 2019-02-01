using System;
using System.Collections.Generic;
using System.Text;

namespace HS.Data.Entities
{
    public class User:BaseEntity<int>
    {
        /// <summary>
        /// 名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 密码
        /// </summary>
        public string Password { get; set; }

        /// <summary>
        /// 盐
        /// </summary>
        public string Salt { get; set; }

        /// <summary>
        /// 性别 1：男 2：女
        /// </summary>
        public int Sex { get; set; }

        /// <summary>
        /// 邮箱
        /// </summary>
        public string Email { get; set; }

        /// <summary>
        /// 移动电话
        /// </summary>
        public string Mobile { get; set; }

        /// <summary>
        /// 权限id
        /// </summary>
        public int RoleId { get; set; }

        /// <summary>
        /// 权限ids
        /// </summary>
        public int RoleIds { get; set; }

        /// <summary>
        /// 最后一次登陆时间
        /// </summary>
        public DateTime LastLoginsDate { get; set; }

        /// <summary>
        /// 登陆错误次数
        /// </summary>
        public int LoginErrorNum { get; set; }

        /// <summary>
        ///  最后一次错误登陆时间
        /// </summary>
        public DateTime LastLoginErrorDate { get; set; }
    }
}
