using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Nw.LiveBackgroundManagement.DataAccessEFCore.Models
{
    /// <summary>
    /// 申请成为主播的审批流程
    /// </summary>
    public class CSUserApply
    {
        public int Id { get; set; }

        /// <summary>
        /// 用户Id
        /// </summary>
        public int CSUserId { get; set; }

        /// <summary>
        /// 状态：1.审批中  2.审批通过   3.审批驳回
        /// </summary>
        public int State { get; set; }

        /// <summary>
        /// 审批意见
        /// </summary>
        public string ApprovalMsg { get; set; }

        /// <summary>
        /// 发起方提交备注
        /// </summary>
        public string Remark { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CreateTime { get; set; }

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
