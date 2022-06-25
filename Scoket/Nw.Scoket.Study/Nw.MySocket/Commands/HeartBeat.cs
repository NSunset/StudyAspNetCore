using SuperSocket.SocketBase.Command;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nw.MySocket
{
    public class HeartBeat : CommandBase<TestOneAppSession, CustomRequestInfo>
    {
        public override string Name => KeyType.HeartBeat.ToString();

        public override void ExecuteCommand(TestOneAppSession session, CustomRequestInfo requestInfo)
        {
            byte[] msg = ResultMsg.Ok(KeyType.HeartBeat,1).GetByte();
            session.Send(msg, 0, msg.Length);
        }
    }
}
