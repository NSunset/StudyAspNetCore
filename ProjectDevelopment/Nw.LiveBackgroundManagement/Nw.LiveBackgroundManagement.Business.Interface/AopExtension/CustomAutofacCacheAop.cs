using Castle.DynamicProxy;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Nw.LiveBackgroundManagement.Common.CacheHelper;
using Nw.LiveBackgroundManagement.DataAccessEFCore.Models;
using Nw.LiveBackgroundManagement.Models.ViewModel;
using RedisHelper.Service;

namespace Nw.LiveBackgroundManagement.Business.Interface.AopExtension
{
    /// <summary>
    /// 记录日志
    /// </summary>
    public class CustomAutofacCacheAop : IInterceptor
    {
        private readonly ILogger<CustomAutofacCacheAop> _logger;
        private readonly RedisHashService _RedisHashService;
        private readonly RedisStringService _RedisStringService;
         
        public CustomAutofacCacheAop(ILogger<CustomAutofacCacheAop> logger,
            RedisHashService redisHashService,
            RedisStringService redisStringService)
        {
            this._logger = logger;
            this._RedisHashService = redisHashService;
            this._RedisStringService = redisStringService;
        }
        public void Intercept(IInvocation invocation)
        {
            {
                _logger.LogInformation("测试一下。。。");
            }

            invocation.Proceed();


            if (invocation.Method.Name.Equals("SysUserLogin") && (bool)invocation.ReturnValue) //说明这里是登录
            {

                _logger.LogInformation("");

                //如果是登录，就缓存 
                SysUser sysUser = invocation.Arguments[2] as SysUser;
                Dictionary<string, string> menuUrlDictionary = invocation.Arguments[3] as Dictionary<string, string>;
                List<MenueViewModel> menueViewList = invocation.Arguments[4] as List<MenueViewModel>;
                _logger.LogInformation(sysUser.Name + "登录成功");

                string menuUrKey = CacheKeyConstant.GetCurrentUserMenuUrlKeyConstant(sysUser.Id.ToString());
                _RedisHashService.Set(menuUrKey, menuUrlDictionary);

                string menuListKey = CacheKeyConstant.GetCurrentUserMenuListKeyConstant(sysUser.Id.ToString());
                _RedisStringService.Set<List<MenueViewModel>>(menuListKey, menueViewList);
            }
        }
    }
}
