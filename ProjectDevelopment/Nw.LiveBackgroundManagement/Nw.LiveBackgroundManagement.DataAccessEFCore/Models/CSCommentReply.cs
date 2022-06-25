using System;
using System.Collections.Generic;
using System.Text;

namespace Nw.LiveBackgroundManagement.DataAccessEFCore.Models
{
    /// <summary>
    /// 评论回复表
    /// </summary>
    public class CSCommentReply
    {
        /// <summary>
        /// 主键Id
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// 评论编码
        /// </summary>
        public int CommentId { get; set; }

        /// <summary>
        /// 被回复人Id
        /// </summary>
        public int ToUserId { get; set; }

        /// <summary>
        /// 被回复人名
        /// </summary>
        public string ToUserName { get; set; }

        /// <summary>
        /// 回复人Id
        /// </summary>
        public int FromUserId { get; set; }

        /// <summary>
        /// 回复人名
        /// </summary>
        public string FromUserName { get; set; }

        /// <summary>
        /// 回复内容
        /// </summary>
        public string Text { get; set; }

        /// <summary>
        /// 回复人头像
        /// </summary>
        public string ImgUrl { get; set; }

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
