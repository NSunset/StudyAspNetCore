using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

#nullable disable

namespace Nw.EFCore.CodeFirst.Models
{
    [Table("tb_addtable")]
    public partial class Addtable
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
}
