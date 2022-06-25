using System;
using System.Collections.Generic;
using System.Text;

namespace Nw.LiveBackgroundManagement.DataAccessEFCore.Models
{

    /// <summary>
    /// 主播房间
    /// </summary>
    public class CSRoom
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
        /// 房间类别
        /// </summary>
        public virtual CSRoomType CSRoomType { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CreateTime { get; set; }

        /// <summary>
        /// 创建人
        /// </summary>
        public int CreateId { get; set; }

        /// <summary>
        /// 修改时间
        /// </summary>
        public DateTime? LastModifyTime { get; set; }

        /// <summary>
        /// 修改人
        /// </summary>
        public int? LastModifyId { get; set; }

        /// <summary>
        /// 房间图片地址
        /// </summary>
        public string RoomImgUrl { get; set; }
    }
}
