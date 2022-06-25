using System;
using System.Collections.Generic;
using System.Text;

namespace Nw.LiveBackgroundManagement.Models.CSEnum
{
    /// <summary>
    /// 审批状态
    /// </summary>
    public enum ApprovalStatusEnum
    { 
        /// <summary>
        /// 普通用户 无需要审批
        /// </summary>
        NoApproval = 0,
        /// <summary>
        /// 审批中
        /// </summary>
        UnderApproval = 1,
        /// <summary>
        /// 审批通过
        /// </summary>
        Approved = 2,
        /// <summary>
        /// 审批驳回
        /// </summary>
        ApprovalRejected = 3
    }
}
