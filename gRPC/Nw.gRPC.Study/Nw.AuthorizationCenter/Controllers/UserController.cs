using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Nw.gRPC.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Nw.AuthorizationCenter.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private IJwtService _jwtService;
        public UserController(IJwtService jwtService)
        {
            _jwtService = jwtService;
        }

        /// <summary>
        /// 登录
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [Route("Login")]
        public string Login(string userName, string password)
        {
            if (userName.Equals("张三") && password.Equals("123456"))
            {
                UserDto userDto = new UserDto
                {
                    Name = userName,
                    Id = 10
                };
                //登录成功,返回token
                string token = _jwtService.GetToken(userDto);
                return token;
            }
            else
            {
                //登录失败
                return "";
            }
        }
    }
}
