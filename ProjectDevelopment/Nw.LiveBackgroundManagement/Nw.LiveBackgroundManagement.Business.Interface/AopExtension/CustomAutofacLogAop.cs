using Castle.DynamicProxy;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nw.LiveBackgroundManagement.Business.Interface.AopExtension
{
    /// <summary>
    /// 记录日志
    /// </summary>
    public class CustomAutofacLogAop : IInterceptor
    {

        private readonly ILogger<CustomAutofacLogAop> _logger;

        public CustomAutofacLogAop(ILogger<CustomAutofacLogAop> logger)
        {
            _logger = logger;
        }
        public void Intercept(IInvocation invocation)
        {
            {
                _logger.LogInformation("测试一下。。。");
            }
            invocation.Proceed();
            { 
                
            }
        }
    }
}
