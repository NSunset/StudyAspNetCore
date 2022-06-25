using Nw.LiveBackgroundManagement.DataAccessEFCore.Models;
using Nw.LiveBackgroundManagement.Models.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Nw.LiveReceptionManage.AuthorizationCenter.JWTHelper
{
    public interface IJwtService
    {
        LoginResultViewModel GetToken(CSUser cSUser);

        LoginResultViewModel RefreshToken(CSUser cSUser);
    }
}
