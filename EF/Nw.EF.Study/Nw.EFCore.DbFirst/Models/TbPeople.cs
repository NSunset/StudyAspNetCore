using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

#nullable disable

namespace Nw.EFCore.DbFirst.Models
{
    [Table("tb_people")]
    public partial class TbPeople
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime? CreateTime { get; set; }
    }
}
