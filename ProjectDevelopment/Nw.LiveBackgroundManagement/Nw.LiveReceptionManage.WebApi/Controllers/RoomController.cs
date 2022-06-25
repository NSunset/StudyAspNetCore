using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Security.Claims;
using System.Threading.Tasks;
using Nw.LiveBackgroundManagement.Business.Interface;
using Nw.LiveBackgroundManagement.Common.Api;
using Nw.LiveBackgroundManagement.DataAccessEFCore.Models;
using Nw.LiveBackgroundManagement.Models.Query;
using Nw.LiveBackgroundManagement.Models.ViewModel;
using Microsoft.AspNetCore.Authorization;

namespace Nw.LiveReceptionManagement.WebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class RoomController : ControllerBase
    {
        private readonly ICSRoomService _ICSRoomService = null;

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="iCSStatisticsService"></param>
        public RoomController(ICSRoomService iCSRoomService)
        {
            this._ICSRoomService = iCSRoomService;
        }

        /// <summary>
        /// 前台用户对应房间信息
        /// </summary>
        /// <returns></returns>
        [Route("GetCurrentRoom")]
        [HttpGet]
        public IActionResult GetCurrentRoom()
        {  
            var rsult = _ICSRoomService.GetCurrentRoom(CurrentUser.Id);
            return new JsonResult(rsult);
        }

        /// <summary>
        /// 房间信息查询
        /// </summary>
        /// <returns></returns>
        [Route("UpdateRoom")]
        [HttpPost]
        [AllowAnonymous]
        public IActionResult UpdateRoom(CSRoomModel room)
        {
            var result = _ICSRoomService.UpdateRoom(room);
            return new JsonResult(result);
        }
    }
}
