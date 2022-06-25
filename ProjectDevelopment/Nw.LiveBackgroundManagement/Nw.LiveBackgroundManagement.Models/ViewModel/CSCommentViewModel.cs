using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nw.LiveBackgroundManagement.Models.ViewModel
{
    public class CSCommentViewModel
    {
        /// <summary>
        /// 评论ID
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// 评论内容
        /// </summary>
        public string Text { get; set; }

        /// <summary>
        /// 被评论人
        /// </summary>
        public int ToUserId { get; set; }

        /// <summary>
        /// 被评论人
        /// </summary>
        public string ToUserName { get; set; }

        //发送评论人
        public int FromUserId { get; set; }

        /// <summary>
        /// 发送评论人
        /// </summary>
        public string FromUserName { get; set; }

        /// <summary>
        /// 作品Id
        /// </summary>
        public int LiveWorksId { get; set; }

        /// <summary>
        /// 评论类型
        /// </summary>
        public int CSCommentType { get; set; }


        /// <summary>
        /// 弹幕时间点
        /// </summary>
        public long BulletChatTime { get; set; }

        /// <summary>
        /// 评论时间
        /// </summary>
        public DateTime CreateTime { get; set; }

        /// <summary>
        /// 评论时间
        /// </summary>
        public string StrCreateTime
        {
            get
            {
                return CreateTime.ToString("yyyy-MM-dd HH:mm ss");
            }
        }

        /// <summary>
        /// 创建人
        /// </summary>
        public int CreatorId { get; set; }

        /// <summary>
        /// 修改时间
        /// </summary>
        public DateTime? LastModifyTime { get; set; }

        /// <summary>
        /// 修改人
        /// </summary>
        public int? LastModifierId { get; set; }

        /// <summary>
        /// 当前正在播放视频的人的Guid 随着关闭Guid也失效
        /// </summary>
        public string TouristGuid { get; set; }
    }
}
