using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Nw.LiveBackgroundManagement.Business.Interface;
using Nw.LiveBackgroundManagement.Common.Api;
using Nw.LiveBackgroundManagement.Models.Query;
using Microsoft.AspNetCore.Authorization;
using Nw.LiveBackgroundManagement.Models.ViewModel;

namespace Nw.LiveReceptionManagement.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class WorkController : ControllerBase
    {
        private readonly ICSUserservice _ICSUserservice = null;

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="iCSStatisticsService"></param>
        public WorkController(ICSUserservice iCSUserservice)
        {
            this._ICSUserservice = iCSUserservice;
        }

        /// <summary>
        /// 用户评论管理
        /// </summary>
        /// <returns></returns>
        [Route("GetCurrentWorkPage")]
        [HttpPost]
        public IActionResult GetCurrentWorkPage(WorkPageQuery query)
        {
            query.UserId = CurrentUser.Id;
            var rsult = _ICSUserservice.GetCurrentWorkPage(query);
            return new JsonResult(rsult);
        }
    }
}
