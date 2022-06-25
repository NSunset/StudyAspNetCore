using System;
using System.Collections.Generic;

#nullable disable

namespace Nw.Account.MicroService.Models
{
    public partial class Account
    {
        public int Id { get; set; }
        public decimal? Residue { get; set; }
        public decimal? Total { get; set; }
        public decimal? Used { get; set; }
        public int? UserId { get; set; }
    }
}
