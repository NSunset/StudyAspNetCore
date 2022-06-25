using Microsoft.AspNetCore.Mvc.Rendering;
using Nw.LiveBackgroundManagement.Common.Api;
using Nw.LiveBackgroundManagement.Models.ViewModel;
using System.Collections.Generic;

namespace Nw.LiveBackgroundManagement.Business.Interface
{
    public interface ICSRoomService : IBaseService
    {
        /// <summary>
        /// 获取主播的列表
        /// </summary>
        /// <returns></returns>
        public IEnumerable<SelectListItem> QueyRoomTypeSelectList();

        /// <summary>
        /// 获取用户的房间信息
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public ApiResult GetCurrentRoom(int userId);

        /// <summary>
        /// 修改房间信息
        /// </summary>
        /// <param name="room"></param>
        /// <returns></returns>
        public ApiResult UpdateRoom(CSRoomModel room);
    }
}
