using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Nw.LiveBackgroundManagement.Business.Interface;
using Nw.LiveBackgroundManagement.Models.ViewModel;
using Nw.LiveBackgroundManagement.Models;
using Nw.LiveBackgroundManagement.WebSite.Utility.CustomPageComponent;
using Nw.LiveBackgroundManagement.Models.CSEnum;
using Nw.LiveBackgroundManagement.WebSite.Utility.Filters;
using Nw.LiveBackgroundManagement.WebSite.Utility.AuthorizationPolicy;
using Microsoft.AspNetCore.Authorization;
using RedisHelper.Service;

namespace Nw.LiveBackgroundManagement.WebSite.Areas.Reception.Controllers
{
    public class CSUserController : Controller
    {

        private readonly ICSRoomService _ICSRoomService = null;
        private readonly ICSUserservice _ICSUserservice = null;
        private readonly IMapper _iMapper;
        //private readonly IDistributedCache _IdistributedCache;

        private ILogger<CSUserController> _Logger = null;
        private RedisStringService _redisString;

        public CSUserController(
            ICSRoomService iCSRoomService, 
            ICSUserservice iCSUserservice, 
            IMapper mapper, 
            ILogger<CSUserController> logger, 
            IDistributedCache distributedCache,
            RedisStringService redisStringService
            )
        {
            this._ICSRoomService = iCSRoomService;
            this._ICSUserservice = iCSUserservice;
            this._iMapper = mapper;
            this._Logger = logger;
            //this._IdistributedCache = distributedCache;
            _redisString = redisStringService;

            _Logger.LogInformation("被构造。。。。");
        }

        public void Index()
        {
            HttpContext.Response.Redirect("CSUserAllView");
        }

        /// <summary>
        /// 前台用户列表
        /// </summary>
        /// <param name="searchString"></param>
        /// <param name="userType"></param>
        /// <param name="url"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        [HttpGet]
        [Authorize(CustomAuthorizationHandler.MenuPolicy)]
        public IActionResult CSUserAllView(string searchString, string userType, string url, int pageIndex = 1, int pageSize = 10)
        {
            PageResult<UserViewModel> pageResult = _ICSUserservice.GetPageReceptionUserList(searchString, userType, url, pageIndex, pageSize);
            CustomPageModel<UserViewModel> model = new()
            {
                PageIndex = pageResult.PageIndex,
                PageSize = pageResult.PageSize,
                RecordCount = pageResult.TotalCount,
                DataList = pageResult.DataList
            };
            //ViewBag.GetRoomTypeSelectList = _ICSRoomService.QueyRoomTypeSelectList();
            ViewBag.searchString = searchString;
            ViewBag.userType = userType;
            return View("~/Areas/Reception/Views/CSUser/CSUserAllView.cshtml", model);
        }


        /// <summary>
        /// 审批列表
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        [HttpGet]
        public IActionResult ApprovalView(int userId)
        {
            List<CSUserApplyViewModel> ApprovalList = _ICSUserservice.ApprovalList(userId);
            return PartialView("~/Areas/Reception/Views/CSUser/ApprovalView.cshtml", ApprovalList);
        }

        [HttpGet]
        [TypeFilter(typeof(CustomLogActionFilterAttribute))]  //业务日志的操作，根据实际情况；
        public IActionResult ApprovalAnchor(int userId, int approvalState, string approvalMsg)
        {
            bool flg = _ICSUserservice.ApprovalUser(userId, (ApprovalStatusEnum)approvalState, approvalMsg);
            if (flg)
            {
                return Json(new AjaxResult()
                {
                    DebugMessage = "操作成功",
                    Result = DoResult.Success
                });
            }

            return Json(new AjaxResult()
            {
                DebugMessage = "操作失败",
                Result = DoResult.Failed
            });
        }


        /// <summary>
        /// 审批详情
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        [HttpGet]
        public IActionResult ApprovalDetailView(int userId)
        {
            List<CSUserApplyViewModel> ApprovalList = _ICSUserservice.ApprovalList(userId);
            return PartialView("~/Areas/Reception/Views/CSUser/ApprovalDetailView.cshtml", ApprovalList);
        }


        /// <summary>
        /// 冻结
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        [HttpGet]
        public IActionResult FreezeUser(int userId)
        {
            bool flg = _ICSUserservice.FreezeUser(userId);
            //使用redis
            _redisString.Set(userId.ToString(), UserStatusEnum.Frozen.ToString());  //记录冻结的用户

            //_IdistributedCache.SetString(userId.ToString(), UserStatusEnum.Frozen.ToString());  //记录冻结的用户
            if (flg)
            {
                return Json(new AjaxResult()
                {
                    DebugMessage = "已冻结",
                    Result = DoResult.Success
                });
            }

            return Json(new AjaxResult()
            {
                DebugMessage = "操作失败",
                Result = DoResult.Failed
            });
        }

        [HttpGet]
        public IActionResult ThawUser(int userId)
        {
            bool flg = _ICSUserservice.ThawUser(userId);
            //使用redis
            _redisString.Remove(userId.ToString());  //在Redis中删除用户的冻结状态

            //_IdistributedCache.Remove(userId.ToString()); //在Redis中删除用户的冻结状态
            if (flg)
            {
                return Json(new AjaxResult()
                {
                    DebugMessage = "已解冻",
                    Result = DoResult.Success
                });
            }

            return Json(new AjaxResult()
            {
                DebugMessage = "操作失败",
                Result = DoResult.Failed
            });
        }

    }
}
