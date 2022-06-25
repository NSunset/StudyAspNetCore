using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Nw.LiveBackgroundManagement.Common.Api;

namespace Nw.LiveBackgroundManagement.Models.Query
{
    public class ApplyPageQuery : Page
    {
        public string StringWord { get; set; }

        public int CurrentUserId { get; set; }
    }
}
