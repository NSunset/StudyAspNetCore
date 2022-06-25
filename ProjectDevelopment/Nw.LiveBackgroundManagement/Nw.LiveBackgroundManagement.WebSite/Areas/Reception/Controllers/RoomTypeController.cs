using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Nw.LiveBackgroundManagement.Business.Interface;
using Nw.LiveBackgroundManagement.DataAccessEFCore.Models;
using Nw.LiveBackgroundManagement.Models;
using Nw.LiveBackgroundManagement.Models.ViewModel;
using Nw.LiveBackgroundManagement.WebSite.Utility.AuthorizationPolicy;
using Nw.LiveBackgroundManagement.WebSite.Utility.CustomPageComponent;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Nw.LiveBackgroundManagement.WebSite.Areas.Reception.Controllers
{
    public class RoomTypeController : Controller
    {
        private readonly ICSRoomService _cSRoomService;
        private readonly IMapper _mapper;

        public RoomTypeController(
            ICSRoomService cSRoomService,
            IMapper mapper
            )
        {
            _cSRoomService = cSRoomService;
            _mapper = mapper;
        }

        public void Index()
        {
            HttpContext.Response.Redirect("RoomTypeView");
        }

        [Authorize(CustomAuthorizationHandler.MenuPolicy)]
        [HttpGet]
        public IActionResult RoomTypeView(string searchString, string url, int pageIndex = 1, int pageSize = 10)
        {
            Expression<Func<CSRoomType, bool>> expression = c => true;// c.Description.Contains(searchString);
            Expression<Func<CSRoomType, int>> expression1 = c => c.Id;
            PageResult<CSRoomType> pageResult = _cSRoomService.QueryPage(expression, pageSize, pageIndex, expression1,false);

            //一般在项目开发的时候，数据库来的数据首先是保存在和数据库表对应的实体集合中；要返回给视图使用，使用展示的时候，一般不会直接使用和数据库对应的实体，会来一个ViewModel，ViewModel就是用来让控制器和视图之间做数据的传值和展示； Dto

            //存在一个转换：Automapper
            CustomPageModel<RoomTypeViewModel> model = new()
            {
                PageIndex = pageResult.PageIndex,
                PageSize = pageResult.PageSize,
                RecordCount = pageResult.TotalCount,
                DataList = _mapper.Map<List<CSRoomType>, List<RoomTypeViewModel>>(pageResult.DataList),
                SearchString = ""
            };

            ViewBag.Url = "RoomType/RoomTypeView";
            return View("~/Areas/Reception/Views/RoomType/RoomTypeView.cshtml", model);
        }

        /// <summary>
        /// 新增或者添加房间类型·
        /// </summary>
        /// <param name="viewModel"></param>
        /// <returns></returns>
        [HttpPost]
        public IActionResult AddRoomType([FromForm] RoomTypeViewModel viewModel)
        {
            CSRoomType addrooType = _mapper.Map<RoomTypeViewModel, CSRoomType>(viewModel);
            if (addrooType.Id > 0)
            {
                _cSRoomService.Update(addrooType);
            }
            else
            {
                _cSRoomService.Insert(addrooType);
            }
            return Json(new AjaxResult()
            {
                Result = DoResult.Success
            });
        }

        [HttpGet]
        public IActionResult DetailRoomtype(int id)
        {
            if (id == 0)
            {
                return PartialView("~/Areas/Reception/Views/RoomType/DetailRoomtype.cshtml", new RoomTypeViewModel());
            }
            else
            {
                CSRoomType cSRoomType = _cSRoomService.Find<CSRoomType>(id);
                RoomTypeViewModel roomTypeViewModel = _mapper.Map<CSRoomType, RoomTypeViewModel>(cSRoomType);
                return PartialView("~/Areas/Reception/Views/RoomType/DetailRoomtype.cshtml", roomTypeViewModel);
            }
        }

        [HttpPost]
        public IActionResult DeleteRoomtype(int id)
        {
            _cSRoomService.Delete<CSRoomType>(id);

            return Json(new AjaxResult()
            {
                Result = DoResult.Success
            });
        }
    }
}
