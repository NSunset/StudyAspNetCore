using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Nw.MyWebSocket.Models
{
    public class ResultMsg
    {
        public bool IsOk { get; set; }

        public object Body { get; set; }
    }
}
