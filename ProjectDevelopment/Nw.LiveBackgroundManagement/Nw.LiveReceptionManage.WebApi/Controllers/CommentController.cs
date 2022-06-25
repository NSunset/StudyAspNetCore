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
using Nw.LiveBackgroundManagement.Models.ViewModel;
using Microsoft.AspNetCore.Authorization;

namespace Nw.LiveReceptionManagement.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class CommentController : ControllerBase
    {
        private readonly ICSUserservice _ICSUserservice = null;

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="iCSStatisticsService"></param>
        public CommentController(ICSUserservice iCSUserservice, ICSRoomService iCSRoomService)
        {
            this._ICSUserservice = iCSUserservice;
        }

        /// <summary>
        /// 用户评论管理
        /// </summary>
        /// <returns></returns>
        [Route("GetCurrentComment")]
        [HttpPost]
        public IActionResult GetCurrentComment(CommentPageQuery query)
        {  
            query.UserId= CurrentUser.Id;
            var rsult = _ICSUserservice.GetCurrentComment(query);
            return new JsonResult(rsult);
        }


        [Route("GetCommentReplyByWorkId")]
        [HttpGet]
        public IActionResult GetCommentReplyByWorkId(int workId)
        {
            var rsult = _ICSUserservice.GetCommentReplyByWorkId(workId);
            return new JsonResult(rsult);
        }

        /// <summary>
        /// 添加弹幕
        /// </summary>
        /// <param name="cSComment"></param>
        /// <returns></returns>
        [Route("AddBulletChat")]
        [HttpPost]
        public IActionResult AddBulletChat(CSCommentViewModel cSComment)
        {
            cSComment.LastModifierId = CurrentUser.Id;
            ApiResult apiResult = _ICSUserservice.AddBulletChat(cSComment);
            return new JsonResult(apiResult);
        }

        //定期获取弹幕
        [Route("GetBulletChat")]
        [HttpGet]
        public IActionResult GetBulletChat(int userid, int cSWorksId, long bulletChatTime)
        {
            ApiResult apiResult = _ICSUserservice.GetBulletChat(userid, cSWorksId, bulletChatTime);
            return new JsonResult(apiResult);
        }
    }
}
