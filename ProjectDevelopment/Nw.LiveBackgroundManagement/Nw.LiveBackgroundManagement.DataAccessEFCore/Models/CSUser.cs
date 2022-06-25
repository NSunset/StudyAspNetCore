using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Nw.LiveBackgroundManagement.DataAccessEFCore.Models
{
    /// <summary>
    /// 用户
    /// </summary>
    public class CSUser
    {
        /// <summary>
        /// 用户Id
        /// </summary>
        public int Id { get; set; }

        [Required]
        [StringLength(40)]
        public string Name { get; set; }

        public string ImgUrl { get; set; }

        [Required]
        [StringLength(64)]
        public string Password { get; set; }

        /// <summary>
        /// 用户类型   1:普通用户   2：主播
        /// </summary>
        public int UserType { get; set; }

        /// <summary>
        /// 1:正常状态  2.冻结  3.删除
        /// </summary>
        public byte Status { get; set; }

        /// <summary>
        /// 审批状态
        /// </summary>
        public int? ApplysState { get; set; }

        [StringLength(20)]
        public string Phone { get; set; }

        [StringLength(20)]
        public string Mobile { get; set; }

        [StringLength(400)]
        public string Address { get; set; }

        [StringLength(100)]
        public string Email { get; set; }

        public long? QQ { get; set; }

        [StringLength(50)]
        public string WeChat { get; set; }

        public byte? Sex { get; set; }

        public DateTime? LastLoginTime { get; set; }

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

        /// <summary>
        /// 申请成为主播的审批流程
        /// </summary>
        public virtual List<CSUserApply> CSUserApply { get; set; }

        /// <summary>
        /// 房间Id
        /// </summary>
        public int? RoomId { get; set; }
        /// <summary>
        /// 主播对应的房间
        /// </summary>
        public virtual CSRoom CurrentUserRoom { get; set; }

    }
}
