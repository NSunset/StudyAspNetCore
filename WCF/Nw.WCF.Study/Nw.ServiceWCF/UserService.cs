using Newtonsoft.Json;
using Nw.InterfaceWCF;
using Nw.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace Nw.ServiceWCF
{
    // 注意: 使用“重构”菜单上的“重命名”命令，可以同时更改代码和配置文件中的类名“UserService”。
    public class UserService : IUserService
    {
        public void AddUser(User user)
        {
            Console.WriteLine($"添加用户信息{JsonConvert.SerializeObject(user)}");

            //执行客户端回调
            ICallBackService callBackService = OperationContext.Current.GetCallbackChannel<ICallBackService>();

            ///添加完后，把用户信息给客户端执行回调操作
            user.Id = 20;
            callBackService.CallBack(user);
        }

        //public void DoWork()
        //{
        //}

        public User Find(int id)
        {
            User user = new User(10)
            {
                Id = id,
                Name = "张三",
                CreateTime = DateTime.Now
            };
            return user;
        }
    }
}
