using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace HS.Data.Command.Account
{
    public class UserLoginCommand
    {
        /// <summary>
        /// 账号
        /// </summary>
        [Required]
        public string Account { get; set; }

        /// <summary>
        /// 密码
        /// </summary>
        [Required]
        public string Password { get; set; }

        /// <summary>
        /// 登陆之前访问的url地址
        /// </summary>
        public string ReturnUrl { get; set; }
    }
}
