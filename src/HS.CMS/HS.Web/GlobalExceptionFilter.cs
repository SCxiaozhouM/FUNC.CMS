using HS.Infrastructure.Log;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace HS.Web
{
    /// <summary>
    /// 全局异常
    /// </summary>
    public class GlobalExceptionFilter : IExceptionFilter
    {
        private readonly ILoggerHelper _loggerHelper;
        public GlobalExceptionFilter(ILoggerHelper loggerHelper)
        {
            this._loggerHelper = loggerHelper;
        }


        public void OnException(ExceptionContext context)
        {
            //记录异常信息
            var source = context.Exception.TargetSite.GetType().FullName;
            var message = context.Exception.ToString();
            _loggerHelper.Error(source, message, context.Exception.GetType().FullName);
            context.HttpContext.Response.StatusCode = HttpStatusCode.InternalServerError.ToInt();
            context.ExceptionHandled = true;
        }
    }
}
