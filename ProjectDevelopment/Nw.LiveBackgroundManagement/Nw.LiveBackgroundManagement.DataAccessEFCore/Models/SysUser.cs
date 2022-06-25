using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Nw.LiveBackgroundManagement.DataAccessEFCore.Models
{
    [Table("SysUser")]
    public partial class SysUser
    {
        public int Id { get; set; }

        [Required]
        [StringLength(20)]
        public string Name { get; set; }

        [Required]
        [StringLength(64)]
        public string Password { get; set; }

        public byte Status { get; set; }

        [StringLength(20)]
        public string Phone { get; set; }

        [StringLength(20)]
        public string Mobile { get; set; }

        [StringLength(50)]
        public string Address { get; set; }

        [StringLength(100)]
        public string Email { get; set; }

        public long? QQ { get; set; }

        [StringLength(50)]
        public string WeChat { get; set; }

        public byte? Sex { get; set; }

        public DateTime? LastLoginTime { get; set; }

        public DateTime CreateTime { get; set; }

        public int CreateId { get; set; }

        public DateTime? LastModifyTime { get; set; }

        public int? LastModifyId { get; set; }
         
        //主表的主键应用--默认生成时主表名称+Id
        //[ForeignKey("Company")]
        public int? CompanyId { get; set; }

        /// <summary>
        /// 用户和菜单直接映射
        /// </summary>
        public virtual ICollection<SysUserMenuMapping> SysUserMenuMapping { get; set; }
        /// <summary>
        /// 用户角色映射
        /// </summary>
        public virtual ICollection<SysUserRoleMapping> SysUserRoleMapping { get; set; }
    }
}
