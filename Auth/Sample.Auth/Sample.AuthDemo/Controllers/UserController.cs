using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Sample.AuthDemo.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        public UserController()
        {

        }

        [Route("login")]
        [HttpPost]
        public async Task<IActionResult> Login(string name, string pwd, string role = "admin", string email = "sfds@qq.com")
        {
            if (name.Equals("admin") && pwd.Equals("123"))
            {

                var identity = new ClaimsIdentity(new List<Claim>
                {
                    new Claim(ClaimTypes.Name,name),
                    new Claim(ClaimTypes.Role,role),
                    new Claim(ClaimTypes.Email,email)
                }, "Custom");


                await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(identity), new AuthenticationProperties
                {
                    ExpiresUtc = DateTime.UtcNow.AddMinutes(30)
                });

                return Ok(new
                {
                    Result = true,
                    Message = "登录成功"
                });
            }

            return Ok(new
            {
                Result = false,
                Message = "登录失败"
            });
        }


        //[Authorize(Roles ="user")]
        //[Authorize(Policy = "adminPolicy")]
        //[Authorize(Policy = "userPolicy")]
        [Authorize(policy: "Email")]
        [Route("Info")]
        [HttpGet]
        public async Task<IActionResult> Info()
        {
            await Task.Yield();
            return Ok(new
            {
                Result = true,
                Data = from i in HttpContext.User.Claims
                       select new
                       {
                           i.Type,
                           i.Value
                       }
            });
        }
    }
}
