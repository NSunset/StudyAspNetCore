using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nw.LiveBackgroundManagement.Models.ViewModel.Hangfire
{
    public class HangfireUserViewModel
    {
        public string Name { get; set; }

        public string Pwd { get; set; }

        public string PwdAddsalt()
        {
            return $"Nw_Hangfire_{Pwd}";
        }
    }
}
