using System;
using System.Collections.Generic;
using System.Text;
using Nw.LiveBackgroundManagement.DataAccessEFCore.Models;

namespace Nw.LiveBackgroundManagement.Models.ViewModel
{
    public class CSRoomModel
    {
        /// <summary>
        /// 房间Id
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// 主播Id
        /// </summary>
        public int UserId { get; set; }

        /// <summary>
        /// 房间名称
        /// </summary>

        public string RoomName { get; set; }

        /// <summary>
        /// 主播描述
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// 房间类型Id
        /// </summary>
        public int CSRoomTypeId { get; set; }
         
        /// <summary>
        /// 房间Banner
        /// </summary>
        public string RoomImgUrl { get; set; }

    }
}
