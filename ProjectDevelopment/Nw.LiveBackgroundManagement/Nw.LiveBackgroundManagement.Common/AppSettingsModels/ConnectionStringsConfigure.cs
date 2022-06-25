using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nw.LiveBackgroundManagement.Common
{
    public class ConnectionStringsConfigure
    {
        public const string Key = "ConnectionStrings";
        public string AuthorityDbContext { get; set; }
        public string HangfireDbContext { get; set; }
    }
}
