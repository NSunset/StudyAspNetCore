using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nw.LiveBackgroundManagement.Models.ViewModel
{
    public class CSWorkViewModel
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
        /// 总评论数
        /// </summary>
        public int CSCommentCount { get; set; }

        /// <summary>
        /// 总弹幕数
        /// </summary>
        public int BulletChatCount { get; set; }

        /// <summary>
        /// 开始时间
        /// </summary>
        public DateTime StarTime { get; set; }

        public string StrStarTime
        {
            get
            {
                return StarTime.ToString("yyyy-MM-dd HH:mm ss");
            }
        }
        /// <summary>
        /// 结束时间
        /// </summary>
        public DateTime EndTime { get; set; }


        public string StrEndTime
        {
            get
            {
                return EndTime.ToString("yyyy-MM-dd HH:mm ss");
            }
        }
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
    }
}
