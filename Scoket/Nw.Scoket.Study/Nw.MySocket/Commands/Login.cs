using log4net.Repository.Hierarchy;
using Newtonsoft.Json;
using SuperSocket.SocketBase.Command;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nw.MySocket
{
    public class Login : CommandBase<TestOneAppSession, CustomRequestInfo>
    {
        public override string Name => KeyType.Login.ToString();
        public override void ExecuteCommand(TestOneAppSession session, CustomRequestInfo requestInfo)
        {
            //session.Logger.Info("哈哈");
            //session.Logger.Error("错误");
            User user = JsonConvert.DeserializeObject<User>(requestInfo.Body);

            Console.WriteLine($"来自客户端的信息：{JsonConvert.SerializeObject(user)}");

            byte[] msg = null;
            if (user.UserName.Equals("张三")&&user.Pwd.Equals("123456"))
            {
                user.Id = 10;
                session.UserId = user.Id;
                msg = ResultMsg.Ok(KeyType.Login,user).GetByte();
            }
            else
            {
                msg = ResultMsg.Error(KeyType.Login,"登录失败，请重试").GetByte();
            }            
            session.Send(msg, 0, msg.Length);
        }
    }
}
