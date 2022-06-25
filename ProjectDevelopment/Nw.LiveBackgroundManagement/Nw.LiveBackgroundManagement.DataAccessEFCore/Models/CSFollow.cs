using System;
using System.Collections.Generic;
using System.Text;

namespace Nw.LiveBackgroundManagement.DataAccessEFCore.Models
{
    /// <summary>
    /// 我的关注
    /// </summary>
    public class CSFollow
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
        /// 被关注者
        /// </summary>
        public int AnchorId { get; set; }

        /// <summary>
        /// 1:关注   2:浏览历史
        /// </summary>
        public int CollAttentionType { get; set; }

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
