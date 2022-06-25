using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Nw.LiveBackgroundManagement.Business.Interface.Hangfire;
using Nw.LiveBackgroundManagement.Business.Services;
using Nw.LiveBackgroundManagement.Common.Api;
using Nw.LiveBackgroundManagement.Common.EncryptHelper;
using Nw.LiveBackgroundManagement.DataAccessEFCore;
using Nw.LiveBackgroundManagement.DataAccessEFCore.Models.Hangfire;
using Nw.LiveBackgroundManagement.Models.ViewModel.Hangfire;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nw.LiveBackgroundManagement.Business.Service.Hangfire
{
    public class HangfireUserService : BaseService, IHangfireUserService
    {
        public HangfireUserService(AuthorityDbContext context) : base(context)
        {
        }

        public ApiResult Login(HangfireUserViewModel input)
        {
            var pwd = MD5Encrypt.Encrypt(input.PwdAddsalt());
            HangfireUser user = _Context.Set<HangfireUser>().FirstOrDefault(c => c.UserName.Equals(input.Name) && c.Pwd.Equals(pwd));
            if (user == null)
            {
                return ApiResult.Error("用户名或密码错误");
            }
            return ApiResult.Ok(user);
        }

        public ApiResult Register(HangfireUserViewModel input)
        {
            if (string.IsNullOrWhiteSpace(input.Name) || string.IsNullOrWhiteSpace(input.Pwd))
            {
                return ApiResult.Error("用户名或密码不能为空");
            }
            var pwd = MD5Encrypt.Encrypt(input.PwdAddsalt());

            HangfireUser hangfireUser = new HangfireUser
            {
                UserName = input.Name,
                Pwd = pwd,
                CreateTime = DateTime.Now
            };
            HangfireUser user = base.Insert<HangfireUser>(hangfireUser);
            return ApiResult.Ok(user);

        }
    }
}
