using System;
using System.Collections.Generic;
using System.Text;

namespace Nw.LiveBackgroundManagement.DataAccessEFCore.Models
{
    /// <summary>
    /// 
    /// </summary>
    public class CSScoreList
    {
        /// <summary>
        /// 积分流水
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        ///主播Id
        /// </summary>
        public int UserId { get; set; }

        /// <summary>
        /// 
        /// </summary> 
        public int CSWorksId { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string UserName { get; set; }

        /// <summary>
        /// 积分
        /// </summary>
        public decimal Integral { get; set; }


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
    }
}
