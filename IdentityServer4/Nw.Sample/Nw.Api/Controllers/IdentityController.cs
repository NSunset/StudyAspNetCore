using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Nw.Api.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class IdentityController : ControllerBase
    {
        [HttpGet]
        //[Authorize]
        //使用策略验证api范围
        [Authorize("apiScope")]
        public IActionResult Get()
        {
            return Ok(from claim in User.Claims
                      select new
                      {
                          claim.Type,
                          claim.Value
                      });
        }
    }
}
