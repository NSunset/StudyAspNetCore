using AutoMapper;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Nw.LiveBackgroundManagement.Business.Interface;
using Nw.LiveBackgroundManagement.Common.Api;
using Nw.LiveBackgroundManagement.DataAccessEFCore;
using Nw.LiveBackgroundManagement.DataAccessEFCore.Models;
using Nw.LiveBackgroundManagement.Models.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nw.LiveBackgroundManagement.Business.Services
{
    public class CSRoomService : BaseService, ICSRoomService
    {
        private readonly IMapper _iMapper;


        public CSRoomService(AuthorityDbContext context,
            IMapper mapper
            ) : base(context)
        {
            _iMapper = mapper;
        }

        /// <summary>
        /// 获取房间类别数据
        /// </summary>
        /// <returns></returns>
        public IEnumerable<SelectListItem> QueyRoomTypeSelectList()
        {
            return _Context.Set<CSRoomType>().Select(item => new SelectListItem()
            {
                Value = item.Id.ToString(),
                Text = item.Text
            });
        }

        /// <summary>
        /// 获取当前用户的房间信息
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns> 
        public ApiResult GetCurrentRoom(int userId)
        {
            CSUser cSUser = Find<CSUser>(userId);
            CSRoom room = _Context.Set<CSRoom>().FirstOrDefault(c => c.Id == cSUser.RoomId);
            return new ApiResult()
            {
                Data = _iMapper.Map<CSRoom, CSRoomModel>(room)
            };
        }

        /// <summary>
        /// 修改房间信息
        /// </summary>
        /// <param name="room"></param>
        /// <returns></returns>
        public ApiResult UpdateRoom(CSRoomModel room)
        {
            CSRoom sRoom = Find<CSRoom>(room.Id);
            sRoom.RoomName = room.RoomName;
            sRoom.Description = room.Description;
            sRoom.RoomImgUrl = room.RoomImgUrl;
            Commit();
            return new ApiResult();
        }
    }
}
