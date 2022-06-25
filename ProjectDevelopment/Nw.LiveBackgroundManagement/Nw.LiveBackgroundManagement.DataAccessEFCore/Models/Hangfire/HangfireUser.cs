using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nw.LiveBackgroundManagement.DataAccessEFCore.Models.Hangfire
{
    public class HangfireUser
    {
        public int Id { get; set; }
        public string UserName { get; set; }
        public string Pwd { get; set; }

        public DateTime CreateTime { get; set; }
    }
}
