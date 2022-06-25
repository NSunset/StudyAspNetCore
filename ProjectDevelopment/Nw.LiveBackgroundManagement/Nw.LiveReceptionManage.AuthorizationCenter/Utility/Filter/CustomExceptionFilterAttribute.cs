using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Logging;
using Nw.LiveBackgroundManagement.Common;
using Nw.LiveBackgroundManagement.Common.Api;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Nw.LiveReceptionManage.AuthorizationCenter.Utility.Filter
{
    public class CustomExceptionFilterAttribute : Attribute, IExceptionFilter
    {
        private readonly ILogger<CustomExceptionFilterAttribute> _logger;
        public CustomExceptionFilterAttribute(ILogger<CustomExceptionFilterAttribute> logger)
        {
            _logger = logger;
        }


        /// <summary>
        /// 全局异常处理
        /// </summary>
        /// <param name="context"></param>
        public void OnException(ExceptionContext context)
        {
            if (!context.ExceptionHandled)
            {
                _logger.LogError("发生异常", context.Exception);
                context.Result = new JsonResult(new ApiResult
                {
                    ErrorMessage = "服务端出错，请重试"
                });
                context.ExceptionHandled = true;
            }
        }
    }
}
