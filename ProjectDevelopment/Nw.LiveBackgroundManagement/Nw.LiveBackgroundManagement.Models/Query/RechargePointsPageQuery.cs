using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Nw.LiveBackgroundManagement.Common.Api;
using Nw.LiveBackgroundManagement.Models.CSEnum;

namespace Nw.LiveBackgroundManagement.Models.Query
{
    public class RechargePointsPageQuery : Page
    { 
        public int UserId { get; set; }
    }
}
