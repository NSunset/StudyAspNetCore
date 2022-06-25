using System;
using System.Collections.Generic;

#nullable disable

namespace Order.MicroService.Models
{
    public partial class Order
    {
        public int Id { get; set; }
        public int? Count { get; set; }
        public decimal? Money { get; set; }
        public int? ProductId { get; set; }
        public int? Status { get; set; }
        public int? UserId { get; set; }
    }
}
