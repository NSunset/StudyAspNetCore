using System;
using System.Collections.Generic;

#nullable disable

namespace Storage.MicroService.Models
{
    public partial class Storage
    {
        public int Id { get; set; }
        public int? ProductId { get; set; }
        public int? Residue { get; set; }
        public int? Total { get; set; }
        public int? Used { get; set; }
    }
}
