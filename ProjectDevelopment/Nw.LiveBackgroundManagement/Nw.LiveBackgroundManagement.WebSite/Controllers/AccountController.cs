using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Nw.LiveBackgroundManagement.Business.Interface;
using Nw.LiveBackgroundManagement.DataAccessEFCore.Models;
using Nw.LiveBackgroundManagement.Models.ViewModel;

namespace Nw.LiveBackgroundManagement.WebSite.Controllers
{
    public class AccountController : Controller
    {

        private readonly ILogger<AccountController> _logger;
        private readonly ICSUserservice _ICSUserservice;
        private readonly ISysUserservice _ISysUserservice;
        private readonly IRoleMenueSevice _IRoleMenueSevice;


        public AccountController(ILogger<AccountController> logger, ICSUserservice iCSUserservice, ISysUserservice iSysUserservice, IRoleMenueSevice iRoleMenueSevice)
        {
            this._logger = logger;
            this._ICSUserservice = iCSUserservice;
            this._ISysUserservice = iSysUserservice;
            this._IRoleMenueSevice = iRoleMenueSevice;
        }

        public IActionResult Index()
        {
            return View();
        }


        [AllowAnonymous]
        [HttpGet]
        public IActionResult Login()
        {
            if (User.Identity!.IsAuthenticated)
            {
                return Redirect("~/RankingList/Index");
            }
            else
            {
                return View();
            }
        }

        /// <summary>
        /// 登录
        /// </summary>
        /// <param name="name"></param>
        /// <param name="password"></param>
        /// <param name="verify"></param>
        /// <returns></returns>
        [HttpPost] 
        public IActionResult Login(string username, string password)
        { 
            Dictionary<string, string> menuUrlDictionary = null;
            SysUser sysUser = null;
            List<MenueViewModel> menueViewList;
            var isLogin = _ISysUserservice.SysUserLogin(username, password, out sysUser, out menuUrlDictionary, out menueViewList);
            if (!isLogin)
            {
                ViewBag.Message = "用户名或密码错误";
                return View();
            }
            else
            {
                //到这儿集已经登录成功了---最好在这儿就计算好这个登录的人，有哪些菜单权限---保存到缓存中去
                List<Claim> claims = new List<Claim>()
                {
                   new Claim(ClaimTypes.Sid,sysUser.Id.ToString()),
                   new Claim(ClaimTypes.Name,sysUser.Name),
                };
                ClaimsPrincipal userPrincipal = new ClaimsPrincipal(new ClaimsIdentity(claims, "Customer"));
                HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, userPrincipal, new AuthenticationProperties
                {
                    ExpiresUtc = DateTime.UtcNow.AddMinutes(30),//过期时间：30分钟  
                }).Wait();//没用await  

                ////设置某一个用户的菜单权限到Redis中去
                //SetMenToRedis(sysUser.Id.ToString(), menuUrlDictionary, menueViewList);
                return base.Redirect("/RankingList/Index");
            }

            { 
                //登陆成功 ---就可以把当前用户的 相关权限就可以缓存起来 
                //建议不要写在这儿；
            }
        }
    }
}
