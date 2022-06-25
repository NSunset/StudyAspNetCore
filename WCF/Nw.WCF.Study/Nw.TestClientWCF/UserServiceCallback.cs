using Nw.TestClientWCF.UserService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nw.TestClientWCF
{
    public class UserServiceCallback : IUserServiceCallback
    {
        public void CallBack(User user)
        {
            Console.WriteLine($"执行添加用户返回信息后的操作:{Newtonsoft.Json.JsonConvert.SerializeObject(user)}");
        }
    }
}
