using Autofac.Extras.DynamicProxy;
using System;
using System.Collections.Generic;
using System.Text;
using Nw.LiveBackgroundManagement.Business.Interface.AopExtension;
using Nw.LiveBackgroundManagement.DataAccessEFCore.Models;
using Nw.LiveBackgroundManagement.Models;
using Nw.LiveBackgroundManagement.Models.CSEnum;
using Nw.LiveBackgroundManagement.Models.ViewModel;

namespace Nw.LiveBackgroundManagement.Business.Interface
{
    [Intercept(typeof(CustomAutofacCacheAop))]
    public interface ISysUserservice : IBaseService
    {
        /// <summary>
        /// 登录后获取当前用户的权限
        /// </summary>
        /// <param name="username"></param>
        /// <param name="password"></param>
        /// <param name="sysUser"></param>
        /// <param name="menuUrlDictionary"></param>
        /// <param name="menueViewList"></param>
        /// <returns></returns>
        public bool SysUserLogin(string username, string password, out SysUser sysUser, out Dictionary<string, string> menuUrlDictionary, out List<MenueViewModel> menueViewList);

        /// <summary>
        /// 获取当前用户的所有菜单信息
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public List<MenueViewModel> FindCurrentUserMenue(int userId);

        /// <summary>
        /// 获取当前用户所有的Url
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public Dictionary<string, string> GetMenuUrlDictionary(int userId);
    }
}
