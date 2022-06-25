using System;
using System.Collections.Generic;
using System.Text;

namespace Nw.LiveBackgroundManagement.Models.CSEnum
{
    /// <summary>
    /// 用户状态
    /// </summary>
    public enum UserStatusEnum
    {
        /// <summary>
        /// 正常状态
        /// </summary>
        Normal = 1,
        /// <summary>
        /// 用户冻结
        /// </summary>
        Frozen = 2,
        /// <summary>
        /// 用户删除
        /// </summary>
        Delete = 3
    }
}
