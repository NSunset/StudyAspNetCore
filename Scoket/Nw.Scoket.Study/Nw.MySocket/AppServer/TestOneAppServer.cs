using Nw.MySocket.ReceiveFilter;
using SuperSocket.SocketBase;
using SuperSocket.SocketBase.Logging;
using SuperSocket.SocketBase.Protocol;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nw.MySocket
{
    /// <summary>
    /// 代表一个和客户端的逻辑连接,需要制定具体的监听Session
    /// </summary>
    public class TestOneAppServer : AppServer<TestOneAppSession, CustomRequestInfo>
    {
        public TestOneAppServer() : base(new DefaultReceiveFilterFactory<CustomReceiveFilter, CustomRequestInfo>())
        {

        }

        protected override void OnStarted()
        {
            //服务端主动发送信息给客户端
            {
                //Task.Factory.StartNew(() =>
                //{
                //    while (true)
                //    {
                //        System.Threading.Thread.Sleep(500);
                //        if (this.GetAllSessions() != null)
                //        {
                //            List<int> userIds = GetAllSessions().Where(a => a.UserId != 0).Select(a => a.UserId).ToList();//获取所有连接且登录的客户端的用户id

                //            //找到所有用户的订单总量

                //            foreach (int item in userIds)
                //            {
                //                TestOneAppSession session = GetAllSessions().FirstOrDefault(a => a.UserId == item);

                //                byte[] msg = ResultMsg.Ok(KeyType.Order, new
                //                {
                //                    Total = 10
                //                }).GetByte();

                //                session.Send(msg, 0, msg.Length);
                //            }
                //        }
                //    }
                //});
            }
            
            base.OnStarted();
        }
    }
}
