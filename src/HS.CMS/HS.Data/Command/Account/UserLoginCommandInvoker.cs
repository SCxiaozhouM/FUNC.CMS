using HS.Infrastructure.Command;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HS.Data.Command.Account
{
    /// <summary>
    /// 登陆命令执行类
    /// </summary>
    public class UserLoginCommandInvoker : ICommandInvoker<UserLoginCommand, UserLoginCommandResult>
    {
        private readonly IContextFactory _context;

        public UserLoginCommandInvoker(
            IContextFactory db)
        {
            this._context = db;
        }

        /// <summary>
        /// 执行登陆命令
        /// </summary>
        /// <param name="command"></param>
        /// <returns></returns>
        public UserLoginCommandResult Execute(UserLoginCommand command)
        {
            if (null != command && !command.Account.IsNullOrWhiteSpace() && !command.Password.IsNullOrWhiteSpace())
            {

                //command.Password = $"{command.Password}{_appConfig.Value.PwdSalt}".GetMd5Hash();
                var userModel = this._context.Create().Users
                                    .Where(u => u.Name==command.Account).FirstOrDefault();
                if (userModel != null && (userModel.Password == (command.Password + userModel.Salt).MD5()))
                {
                    return new UserLoginCommandResult() { UserInfo = userModel };
                }
            }
            return new UserLoginCommandResult("用户名或密码错误!");
        }
    }
}
