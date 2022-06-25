using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Nw.LiveBackgroundManagement.Business.Interface;
using Nw.LiveBackgroundManagement.Common.Api;
using Nw.LiveBackgroundManagement.DataAccessEFCore.Models;
using Nw.LiveBackgroundManagement.Models.ViewModel;
using Microsoft.AspNetCore.Authorization;
using Nw.LiveBackgroundManagement.Models.Query;
using Nw.LiveBackgroundManagement.Common.WechatPayCore;
using Microsoft.AspNetCore.Http;
using Nw.LiveBackgroundManagement.Common.Tool;
using System.Drawing;
using System.IO;
using System.Drawing.Imaging;
using RedisHelper.Service;

namespace Nw.LiveReceptionManage.WebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class CSUserController : ControllerBase
    {
        private readonly ICSUserservice _ICSUserservice = null;
        private readonly ICSRoomService _ICSRoomService = null;
        private readonly RedisStringService _redisStringService;
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="iCSStatisticsService"></param>
        public CSUserController(ICSUserservice iCSUserservice,
            ICSRoomService iCSRoomService,
            RedisStringService redisStringService
            )
        {
            this._ICSUserservice = iCSUserservice;
            this._ICSRoomService = iCSRoomService;
            _redisStringService = redisStringService;
        }

        /// <summary>
        /// 前台用户详情
        /// </summary>
        /// <returns></returns>
        [Route("GetCurrentUser")]
        [HttpGet]
        public IActionResult GetCurrentUser()
        {
            var rsult = _ICSUserservice.CurrentUserDetail(CurrentUser.Id);
            return new JsonResult(rsult);
        }

        [Route("UpdateCurrentUserDetail")]
        [HttpPost]
        public IActionResult UpdateCurrentUserDetail( CSUserViewModel cSUserViewModel)
        {
            ApiResult result = null;
            if (CurrentUser.Id != cSUserViewModel.Id)
            {
                return new JsonResult(new ApiResult() { ErrorMessage = "只能修改自己的账号信息" });
            }
            if (CurrentUser.Id == 0)
            {
                return new JsonResult(new ApiResult() { ErrorMessage = "用户ID没找到" });
            }
            result = _ICSUserservice.UpdateUserDetail(cSUserViewModel);
            return new JsonResult(result);
        }

        /// <summary>
        /// 修改密码
        /// </summary>
        /// <param name="cSUserViewModel"></param>
        /// <returns></returns>
        [Route("UpdateCurrentUserPassword")]
        [HttpPost]
        public IActionResult UpdateCurrentUserPassword( UpdatePass updatePass)
        {
            updatePass.UserId = CurrentUser.Id;
            var rsult = _ICSUserservice.UpdateCurrentPassword(updatePass);
            return new JsonResult(rsult);
        }



        /// <summary>
        /// 申请成为主播
        /// </summary>
        /// <returns></returns>
        [Route("ApplyToAnchor")]
        [HttpPost]
        public IActionResult ApplyToAnchor( CSUserApplyViewModel cSUserApply)
        {
            cSUserApply.CSUserId = CurrentUser.Id;
            var rsult = _ICSUserservice.ApplyToAnchor(cSUserApply);
            return new JsonResult(rsult);
        }

        /// <summary>
        /// 分页获取审批列表
        /// </summary>
        /// <returns></returns>
        [Route("GetApplyPageInfo")]
        [HttpPost]
        public IActionResult GetApplyPageInfo( ApplyPageQuery query)
        {
            query.CurrentUserId = CurrentUser.Id;
            var rsult = _ICSUserservice.CSUserApplyPage(query);
            return new JsonResult(rsult);
        }



        /// <summary>
        /// 获取房间类型的Api
        /// </summary>
        /// <returns></returns>
        [Route("QueyRoomTypeSelectList")]
        [HttpGet]
        public IActionResult QueyRoomTypeSelectList()
        {
            var apiResult = _ICSRoomService.QueyRoomTypeSelectList();
            return new JsonResult(new ApiResult() { Data = apiResult });
        }

        /// <summary>
        /// 我的关注和历史记录
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        [Route("QueyBrowsePage")]
        [HttpPost]
        public IActionResult QueyBrowsePage( BrowsePageQuery query)
        {
            query.UserId = CurrentUser.Id;
            var apiResult = _ICSUserservice.QueyBrowsePage(query);
            return new JsonResult(new ApiResult() { Data = apiResult });
        }

        /// <summary>
        /// 获取主播信息
        /// </summary>
        /// <param name="anchorid"></param>
        /// <returns></returns>
        [Route("GetAnchorById")]
        [HttpGet]
        public IActionResult GetAnchorById(int anchorid)
        {
            ApiResult apiResult = _ICSUserservice.GetAnchorById(anchorid);
            return new JsonResult(apiResult);
        }



        /// <summary>
        /// 获取主播信息
        /// </summary>
        /// <param name="anchorid"></param>
        /// <returns></returns>
        [Route("GetCurretnIntegral")]
        [HttpGet]
        public IActionResult GetCurretnIntegral(int userId)
        {
            return new JsonResult(_ICSUserservice.GetCurretnIntegral(userId));
        }


        [Route("RechargePoints")]
        [HttpPost]
        public IActionResult RechargePoints( CSIntegralRechargeDetailViewModel cSIntegral)
        {
            cSIntegral.UserId = CurrentUser.Id;
            ApiResult apiResult = _ICSUserservice.RechargePoints(cSIntegral);
            return new JsonResult(apiResult);
        }

        /// <summary>
        /// 充值历史记录
        /// </summary>
        /// <param name="pageQuery"></param>
        /// <returns></returns>
        [Route("GetRechargePointsPage")]
        [HttpPost]
        public IActionResult GetRechargePointsPage( RechargePointsPageQuery pageQuery)
        {
            pageQuery.UserId = CurrentUser.Id;
            ApiResult apiResult = _ICSUserservice.GetRechargePointsPage(pageQuery);
            return new JsonResult(apiResult);
        }


        /// <summary>
        /// 获取订单状态
        /// </summary>
        /// <param name="rediskey"></param>
        /// <returns></returns>
        [Route("GetOrderState")]
        [HttpGet]
        public IActionResult GetOrderState(string orderNum)
        {
            return new JsonResult(new ApiResult()
            {
                Data = "支付成功了"
            });
        }


        /// <summary>
        /// 微信发过来的支付回调
        /// </summary>
        /// <returns></returns>
        [Route("PayNotify")]
        [HttpPost]
        [AllowAnonymous]
        public async Task PayNotify()
        {
            //如果支付成功了，修改订单的状态；已支付状态；
            WxPayData wxPayData = null;
            WxPayData res = new WxPayData();
            res.SetValue("return_code", wxPayData.GetValue("return_code"));
            res.SetValue("return_msg", wxPayData.GetValue("return_msg"));
            HttpContext.Response.ContentType = "application/xml";
            string xmlResult = res.ToXml();
            Console.WriteLine($"微信支付回调结果：{xmlResult}");
            await HttpContext.Response.WriteAsync(xmlResult);
        }

        /// <summary>
        /// 获取Reddis中的支付链接
        /// </summary>
        /// <param name="rediskey"></param>
        /// <returns></returns>
        [Route("GetPayQRCode")]
        [HttpGet]
        [AllowAnonymous]
        public IActionResult GetPayQRCode(string rediskey)
        {
            string payUrl = _redisStringService.Get(rediskey);
            if (!string.IsNullOrWhiteSpace(payUrl))
            {
                payUrl = payUrl.Trim('"');
            }
            else
            {
                return null;
            }
            Bitmap bitmap = GetQRCodeHelper.GetQRCode(payUrl, 10);
            MemoryStream stream = new MemoryStream();
            bitmap.Save(stream, ImageFormat.Gif);
            return File(stream.ToArray(), "image/gif");//返回FileContentResult图片
        }

    }
}
