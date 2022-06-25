using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nw.LiveBackgroundManagement.Models.ViewModel
{
    public class CSIntegralRechargeViewModel
    {
        /// <summary>
        /// 主键
        /// </summary>
        public int Id { get; set; }
         
        public int UserId { get; set; }
        /// <summary>
        /// 充值总额
        /// </summary>
        public decimal? AmountCount { get; set; }

        /// <summary>
        /// 积分总数量
        /// </summary>
        public int? IntegralCount { get; set; }

        /// <summary>
        /// 已使用积分
        /// </summary>
        public int? UserIntgralCount { get; set; }
         
        /// <summary>
        /// 评论时间
        /// </summary>
        public DateTime? CreateTime { get; set; }

        /// <summary>
        /// 创建人
        /// </summary>
        public int? CreatorId { get; set; }

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
