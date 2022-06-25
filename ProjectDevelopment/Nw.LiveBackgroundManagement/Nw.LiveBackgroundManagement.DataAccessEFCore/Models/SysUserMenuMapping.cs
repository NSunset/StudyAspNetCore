using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Nw.LiveBackgroundManagement.DataAccessEFCore.Models
{
    [Table("SysUserMenuMapping")]
    public partial class SysUserMenuMapping
    {
        public int Id { get; set; }

        public int SysUserId { get; set; }

        public int SysMenuId { get; set; }

        public virtual SysMenu SysMenu { get; set; }
    }
}
