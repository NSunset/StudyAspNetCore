using System;
using System.Collections.Generic;
using System.Text;

namespace Nw.LiveBackgroundManagement.DataAccessEFCore.Models
{
    /// <summary>
    /// 评论+弹幕
    /// </summary>
    public class CSComment
    {
        /// <summary>
        /// 评论ID
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// 作品Id
        /// </summary>
        public int CSWorksId { get; set; }

        /// <summary>
        /// 评论内容
        /// </summary>
        public string Text { get; set; }

        /// <summary>
        /// 对某主播的评论
        /// </summary>
        public int ToUserId { get; set; }

        /// <summary>
        /// 被评论人
        /// </summary>
        public string ToUserName { get; set; }

        /// <summary>
        /// 谁发送的评论
        /// </summary>
        public int FromUserId { get; set; }


        /// <summary>
        /// 发送评论人
        /// </summary>
        public string FromUserName { get; set; }


        /// <summary>
        /// 评论人头像
        /// </summary>
        public string ImgUrl { get; set; }

        /// <summary>
        /// 评论人
        /// </summary>
        public int CSCommentType { get; set; }

        /// <summary>
        /// 评论时间
        /// </summary>
        public DateTime CreateTime { get; set; }

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
    }
}
