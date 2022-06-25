using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Nw.LiveBackgroundManagement.DataAccessEFCore.Models
{
    [Table("SysRoleMenuMapping")]
    public partial class SysRoleMenuMapping
    {
        public int Id { get; set; }

        public int SysRoleId { get; set; }

        public int SysMenuId { get; set; }
        public virtual SysMenu SysMenu { get; set; }
    }
}
