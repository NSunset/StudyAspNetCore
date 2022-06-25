using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Sample.Client.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        public ProductController()
        {

        }

        //[Authorize(Roles ="user")]
        [Authorize(Policy = "CustomPolicy")]
        [Route("Get")]
        [HttpGet]
        public IActionResult Get()
        {
            return Ok(new
            {
                IsOk = true,
                Data = from u in HttpContext.User.Claims
                       select new
                       {
                           u.Type,
                           u.Value
                       }
            });
        }

        [Authorize(Policy = "CustomPolicy")]
        [Route("Add")]
        [HttpPost]
        public IActionResult Add()
        {
            return Ok(new
            {
                IsOk = true,
                Data = from u in HttpContext.User.Claims
                       select new
                       {
                           u.Type,
                           u.Value
                       }
            });
        }
    }
}
