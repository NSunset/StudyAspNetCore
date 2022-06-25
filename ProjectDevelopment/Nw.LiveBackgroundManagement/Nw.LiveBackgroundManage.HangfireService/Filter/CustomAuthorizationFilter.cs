using Hangfire.Annotations;
using Hangfire.Dashboard;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Http;
using Nw.LiveBackgroundManagement.Business.Interface.Hangfire;
using Nw.LiveBackgroundManagement.Common.Api;
using Nw.LiveBackgroundManagement.Common.EncryptHelper;
using Nw.LiveBackgroundManagement.DataAccessEFCore.Models.Hangfire;
using Nw.LiveBackgroundManagement.Models.ViewModel.Hangfire;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Nw.LiveBackgroundManage.HangfireService.Filter
{
    public class CustomAuthorizationFilter : IDashboardAuthorizationFilter
    {
        private IHangfireUserService _hangfireUserService;

        private static string userInfo;
        private static DateTime? expiredTime;

        public CustomAuthorizationFilter(IHangfireUserService hangfireUserService)
        {
            _hangfireUserService = hangfireUserService;
        }

        public bool Authorize([NotNull] DashboardContext context)
        {
            var httpContext = context.GetHttpContext();

            if (!string.IsNullOrWhiteSpace(userInfo))
            {
                if (DateTime.Now > expiredTime)
                {
                    //重新登录
                    userInfo = string.Empty;
                    expiredTime = null;
                    SetChallengeResponse(httpContext);
                    return false;
                }
                var input = GetRequestUser(httpContext);
                //string name = httpContext.User.Claims.FirstOrDefault(x => x.Type == "hangfire_login_name").Value;

                //string pwd = httpContext.User.Claims.FirstOrDefault(x => x.Type == "hangfire_login_pwd").Value;
                if (input != null)
                {
                    string pwd = MD5Encrypt.Encrypt(input.PwdAddsalt());
                    var flage = userInfo == MD5Encrypt.Encrypt($"valide_{input.Name}_{pwd}");
                    return flage;
                }
            }

            var result = Validate(httpContext);
            if (result != null && result.Success)
            {
                HangfireUser hangfireUser = result.Data as HangfireUser;
                //var claims = new List<Claim>()
                //{
                //    new Claim("hangfire_login_name",hangfireUser.UserName),
                //    new Claim("hangfire_login_pwd",hangfireUser.Pwd)
                //};
                //var userPrincipal= new ClaimsPrincipal(new ClaimsIdentity(claims));

                //httpContext.User = userPrincipal;
                userInfo = MD5Encrypt.Encrypt($"valide_{hangfireUser.UserName}_{hangfireUser.Pwd}");

                expiredTime = DateTime.Now.AddMinutes(30);
                return true;
            }
            return false;

        }

        private HangfireUserViewModel GetRequestUser(HttpContext httpContext)
        {
            var header = httpContext.Request.Headers["Authorization"];

            if (string.IsNullOrWhiteSpace(header))
            {
                return null;
            }

            var authValues = System.Net.Http.Headers.AuthenticationHeaderValue.Parse(header);

            if (!"Basic".Equals(authValues.Scheme, StringComparison.InvariantCultureIgnoreCase))
            {
                return null;
            }

            var parameter = Encoding.UTF8.GetString(Convert.FromBase64String(authValues.Parameter));
            var parts = parameter.Split(':');

            if (parts.Length < 2)
            {
                return null;
            }
            return new HangfireUserViewModel
            {
                Name = parts[0],
                Pwd = parts[1]
            };
        }

        private ApiResult Validate(HttpContext httpContext)
        {
            var input = GetRequestUser(httpContext);
            if (input != null)
            {
                ApiResult result = _hangfireUserService.Login(input);

                if (result.Success)
                {
                    return result;
                }
            }
            SetChallengeResponse(httpContext);
            return null;
        }

        private void SetChallengeResponse(HttpContext httpContext)
        {
            httpContext.Response.StatusCode = 401;

            httpContext.Response.Headers.Append("WWW-Authenticate", "Basic realm=\"Hangfire Dashboard\"");
        }
    }

}
