using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nw.LiveBackgroundManagement.Models.ViewModel
{
    public class CSFollowViewModel
    {

        /// <summary>
        /// 数据Id
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// 关注者
        /// </summary>
        public int UserId { get; set; }

        /// <summary>
        /// 被关注者  主播Id
        /// </summary>
        public int AnchorId { get; set; }

        /// <summary>
        /// 主播名字
        /// </summary>
        public string AnchorName { get; set; }
         
        /// <summary>
        /// 评论时间
        /// </summary>
        public DateTime CreateTime { get; set; }

        /// <summary>
        /// 创建人
        /// </summary>
        public int CreatorId { get; set; }

        /// <summary>
        /// 直播状态
        /// </summary>
        public int LiveState { get; set; }
         
        /// <summary>
        /// 修改时间
        /// </summary>
        public DateTime? LastModifyTime { get; set; }

        /// <summary>
        /// 房间Banner
        /// </summary>
        public string ImgUrl { get; set; }

        /// <summary>
        /// 房间名称
        /// </summary>
        public string RoomName { get; set; }
    }
}
