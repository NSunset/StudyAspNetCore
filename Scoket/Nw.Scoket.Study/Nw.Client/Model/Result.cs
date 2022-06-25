using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nw.Client
{
    public class Result
    {
        public bool IsOk { get; set; }

        public int Type { get; set; }

        public object Msg { get; set; }
    }
}
