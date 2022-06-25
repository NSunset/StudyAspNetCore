using SuperSocket.SocketBase;
using SuperSocket.SocketBase.Protocol;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nw.MySocket
{
    /// <summary>
    /// 监听客户端连接
    /// </summary>
    public class TestOneAppSession : AppSession<TestOneAppSession, CustomRequestInfo>
    {
        public int UserId { get; set; }
    }
}
