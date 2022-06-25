using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Nw.LiveBackgroundManagement.Business.Interface;
using Nw.LiveBackgroundManagement.Common;
using Nw.LiveBackgroundManagement.DataAccessEFCore.Models;
using Nw.LiveBackgroundManagement.Models;
using Nw.LiveBackgroundManagement.Models.CSEnum;
using Nw.LiveBackgroundManagement.Models.ViewModel;
using Nw.LiveBackgroundManagement.DataAccessEFCore;

namespace Nw.LiveBackgroundManagement.Business.Services
{
    public class SysUserservice : BaseService, ISysUserservice
    {
        private readonly IMapper _iMapper;
        public SysUserservice(AuthorityDbContext context, IMapper mapper) : base(context)
        {
            _iMapper = mapper;
        }


        /// <summary>
        /// 密码肯定是需要MD5加密的   然后查询的时候需要使用MD5加密后的数据
        /// </summary>
        /// <param name="username"></param>
        /// <param name="password"></param>
        /// <param name="sysUser"></param>
        /// <param name="menuUrlDictionary"></param>
        /// <param name="menueViewList"></param>
        /// <returns></returns>
        public virtual bool SysUserLogin(string username, string password, out SysUser sysUser, out Dictionary<string, string> menuUrlDictionary, out List<MenueViewModel> menueViewList)
        {
            IQueryable<SysUser> sysUserlist = Query<SysUser>(u => u.Name.Equals(username)
          && u.Password.Equals(password)
          && u.Status == 1);
            if (sysUserlist != null && sysUserlist.Count() > 0)
            {
                sysUser = sysUserlist.FirstOrDefault();
                Console.WriteLine("**********************************************");
                {
                    #region 逐个获取
                    //SysUser currentUser = sysUserlist.FirstOrDefault();
                    //IQueryable<int> roleIds = Query<SysUserRoleMapping>(u => u.SysUserId.Equals(currentUser.Id)).Select(r => r.SysRoleId);
                    //var menlist = Query<SysRoleMenuMapping>(r => roleIds.Contains(r.SysRoleId)).Select(r => r.SysMenu); 
                    #endregion
                }
                Console.WriteLine("**********************************************");

                //在这里缓存  可以----你们觉得我们这里可以如何做？  要做的高端点；
                menuUrlDictionary = GetMenuUrlDictionary(sysUser.Id); //这里是记录了当前用户能都使用的功能  对应的URl地址
                menueViewList = FindCurrentUserMenue(sysUser.Id); //获取当前用户能够看到的  菜单  分层级菜单·
                return true;
            }
            else
            {
                menuUrlDictionary = null;
                sysUser = null;
                menueViewList = null;
                return false;
            }
        }

        /// <summary>
        /// 这里是用来作为绑定左侧菜单的数据源
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public List<MenueViewModel> FindCurrentUserMenue(int userId)
        {
            List<MenueViewModel> menueViewModels = new List<MenueViewModel>();

            var menuIdlist = _Context.Set<SysUserRoleMapping>().Where(u => u.SysUserId == userId).Select(r => r.SysRole).SelectMany(m => m.SysRoleMenuMapping).Select(m => m.SysMenuId).ToList();

            RecursionMenue(menueViewModels, menuIdlist);
            return menueViewModels;
        }

        /// <summary>
        /// 递归所有的菜单
        /// </summary>
        /// <param name="menueViewModels"></param>
        /// <param name="userId"></param>
        /// <param name="parentIds"></param>
        private void RecursionMenue(List<MenueViewModel> menueViewModels, List<int> menuIdlist, int parentIds = 0)
        {
            var menueQuery = _Context.Set<SysMenu>().Where(m => parentIds == m.ParentId && menuIdlist.Contains(m.Id)).OrderBy(m => m.Sort).ToList();
            if (menueQuery != null && menueQuery.Count() > 0)
            {
                foreach (SysMenu menue in menueQuery)
                {
                    MenueViewModel menueViewModel = new MenueViewModel()
                    {
                        Id = menue.Id,
                        Text = menue.Text,
                        Url = menue.Url,
                        Sort = menue.Sort,
                        MenuIcon = menue.MenuIcon,
                        Description = menue.Description,
                        MenuLevel = menue.MenuLevel,
                        MenuType = menue.MenuType,
                        SourcePath = menue.SourcePath,
                        Status = menue.Status
                    };
                    menueViewModels.Add(menueViewModel);
                    RecursionMenue(menueViewModel.SubMenueViewModel, menuIdlist, menueViewModel.Id);
                }
            }
        }



        public Dictionary<string, string> GetMenuUrlDictionary(int userId)
        {
            var sysUserlist = Query<SysUser>(u => u.Id == userId
        && u.Status == 1);

            Dictionary<string, string> MenuRrlDictionary = new Dictionary<string, string>();

            #region 1.通过用户找角色再找出菜单 
            var menuUrl1List = sysUserlist.First().SysUserRoleMapping.Select(r => r.SysRole)
                .SelectMany(m => m.SysRoleMenuMapping)
                .Select(m => m.SysMenu);

            foreach (var menuUrl in menuUrl1List)
            {
                MenuRrlDictionary.Add($"{menuUrl.Id}_{menuUrl.Text}", menuUrl.Url);
            }

            #endregion

            #region 2.通过用户直接找出 菜单 
            //List<Dictionary<string, string>> 

            var menuUrl2List = sysUserlist.First()
              .SysUserMenuMapping
              .Select(r => r.SysMenu);
            foreach (var menuUrl in menuUrl2List)
            {
                MenuRrlDictionary.Add($"{menuUrl.Id}_{menuUrl.Text}", menuUrl.Url);
            }

            #endregion
            return MenuRrlDictionary;
        }
    }
}
