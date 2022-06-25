using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Nw.LiveBackgroundManagement.Models.ViewModel
{
    public class UpdatePass
    {
        public int UserId { get; set; }
        public string OldPass { get; set; } 
        public string NewPass { get; set; }
        public string NewPass1 { get; set; } 
    }
}
