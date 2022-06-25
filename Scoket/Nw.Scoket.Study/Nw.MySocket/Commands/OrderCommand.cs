using SuperSocket.SocketBase.Command;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nw.MySocket
{
    [LogTimeCommandFilter]
    public class OrderCommand : CommandBase<TestOneAppSession, CustomRequestInfo>
    {
        public override string Name => KeyType.Order.ToString();
        public override void ExecuteCommand(TestOneAppSession session, CustomRequestInfo requestInfo)
        {
            //根据session的用户ID，来获取用户的订单总数
            int userid = session.UserId;
            int orderCount = 10;

            byte[] msg = ResultMsg.Ok(KeyType.Order, new { Total = orderCount }).GetByte();
            session.Send(msg, 0, msg.Length);


        }
    }
}
