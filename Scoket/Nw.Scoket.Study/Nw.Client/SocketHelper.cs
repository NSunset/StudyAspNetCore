using Newtonsoft.Json;
using SuperSocket.ClientEngine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Nw.Client
{
    public class SocketHelper
    {
        private AsyncTcpSession _asyncTcpSession;
        public SocketHelper()
        {
            IPEndPoint endPoint = new IPEndPoint(IPAddress.Parse("127.0.0.1"), 2022);

            _asyncTcpSession = new AsyncTcpSession();
            _asyncTcpSession.Connect(endPoint);

            _asyncTcpSession.DataReceived += (send, e) =>
            {
                string msg = Encoding.UTF8.GetString(e.Data, 0, e.Length);

                Result result = JsonConvert.DeserializeObject<Result>(msg);
                Console.WriteLine($"服务端返回消息：{JsonConvert.SerializeObject(result)}");
            };
        }

        public void Send(Input input)
        {
            byte[] msg = input.GetByte();
            _asyncTcpSession.Send(msg, 0, msg.Length);
        }

        public void SendError(Input input)
        {
            string text = JsonConvert.SerializeObject(input);
            byte[] msg = Encoding.UTF8.GetBytes(text); ;
            _asyncTcpSession.Send(msg, 0, msg.Length);
        }
    }
}
