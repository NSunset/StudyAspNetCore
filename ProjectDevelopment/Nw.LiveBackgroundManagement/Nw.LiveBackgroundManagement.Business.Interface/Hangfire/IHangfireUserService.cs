using Nw.LiveBackgroundManagement.Common.Api;
using Nw.LiveBackgroundManagement.Models.ViewModel.Hangfire;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nw.LiveBackgroundManagement.Business.Interface.Hangfire
{
    public interface IHangfireUserService : IBaseService
    {
        ApiResult Login(HangfireUserViewModel user);

        ApiResult Register(HangfireUserViewModel user);
    }
}
