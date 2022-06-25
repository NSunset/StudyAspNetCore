using System;
using System.Collections.Generic;
using System.Text;

namespace Nw.LiveBackgroundManagement.DataAccessEFCore.Models
{
    /// <summary>
    /// 主播作品
    /// </summary>
    public class CSWorks
    {
        /// <summary>
        /// 作品Id
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// 主播Id
        /// </summary>
        public int UserId { get; set; }

        /// <summary>
        /// 作品类型
        /// </summary>
        public int WorkType { get; set; }

        /// <summary>
        /// 作品名称
        /// </summary>
        public string WorkName { get; set; }

        /// <summary>
        /// 开始时间
        /// </summary>
        public DateTime StarTime { get; set; }

        /// <summary>
        /// 结束时间
        /// </summary>
        public DateTime EndTime { get; set; }
         
        /// <summary>
        /// 创建时间
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

        /// <summary>
        /// 评论弹幕树
        /// </summary>
        public virtual List<CSComment> CSComment { get; set; }

        /// <summary>
        /// 评论弹幕树
        /// </summary>
        public virtual List<CSScoreList> CSScoreList { get; set; }
    }
}
