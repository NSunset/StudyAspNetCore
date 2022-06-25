using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Sample.Common.IServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Sample.JwtDemo.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IJwtService jwtService;

        public UserController(IJwtService jwtService)
        {
            this.jwtService = jwtService;
        }

        [Route("Login")]
        [HttpPost]
        public IActionResult Login(string name, string pwd, string role = "admin")
        {
            bool isOk = false;
            object data = null;
            if (name.Equals("admin") && pwd.Equals("123"))
            {
                var jwtTokenModel = jwtService.GetToken(new List<Claim>
                {
                    new Claim(ClaimTypes.Role,role),
                    new Claim(ClaimTypes.Name,name)
                });
                isOk = true;
                data = jwtTokenModel.Token;
            }
            return Ok(new
            {
                IsOk = isOk,
                Data = data
            });
        }
    }
}
