using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Nw.LiveBackgroundManagement.Business.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Nw.LiveBackgroundManagement.WebSite.Utility.AuthorizationPolicy
{
    public class CustomAuthorizationHandler : AuthorizationHandler<CustomAuthorizationRequirement>
    {
        /// <summary>
        /// 菜单策略
        /// </summary>
        public const string MenuPolicy = "MenuPermissions";

        private readonly ISysUserservice _SysUserservice;
        /// <summary>
        /// 构造函数
        /// </summary>
        public CustomAuthorizationHandler(ISysUserservice sysUserservice)
        {
            this._SysUserservice = sysUserservice;
        }

        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, CustomAuthorizationRequirement requirement)
        {
            Claim claim = context.User.Claims.FirstOrDefault(u => u.Type == ClaimTypes.Sid);

            if (claim==null)
            {
                //没有登录
                return Task.CompletedTask;//验证不通过
            }
            int userid = 0;
            if (!string.IsNullOrWhiteSpace(claim.Value))
            {
                userid = Convert.ToInt32(claim.Value);
            }

            Dictionary<string, string> menuRuls = _SysUserservice.GetMenuUrlDictionary(userid);

            DefaultHttpContext httpContext = (DefaultHttpContext)context.Resource;
            if (menuRuls.ContainsValue(httpContext.Request.Path))
            {
                //用户有菜单url权限
                context.Succeed(requirement);
            }
            //用户没有权限
            return Task.CompletedTask;
        }
    }
}
