using SuperSocket.SocketBase.Protocol;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nw.MySocket
{
    public class CustomRequestInfo : IRequestInfo
    {
        public string Key { get; set; }

        public string Body { get; set; }
    }
}
