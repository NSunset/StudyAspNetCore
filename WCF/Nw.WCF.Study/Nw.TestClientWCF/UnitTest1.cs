using Microsoft.VisualStudio.TestTools.UnitTesting;
using Nw.TestClientWCF.UserService;
using System;
using System.ServiceModel;

namespace Nw.TestClientWCF
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestMethod1()
        {
            //WCF非标准写法
            {
                //using (UserServiceClient userServiceClient = new UserServiceClient())
                //{
                //    userServiceClient.AddUser(new User
                //    {
                //        Id = 10,
                //        Name = "张三",
                //        CreateTime = DateTime.Now
                //    });

                //    User user = userServiceClient.Find(15);
                //    Console.WriteLine($"客户端查询返回用户信息:{Newtonsoft.Json.JsonConvert.SerializeObject(user)}");
                //}
            }

            //WCF调用标准写法
            {
                UserServiceClient userServiceClient = null;
                try
                {
                    //配置回调操作
                    InstanceContext instanceContext = new InstanceContext(new UserServiceCallback());

                    userServiceClient = new UserServiceClient(instanceContext);

                    userServiceClient.AddUser(new User
                    {
                        Id = 10,
                        Name = "张三",
                        CreateTime = DateTime.Now
                    });

                    User user = userServiceClient.Find(15);
                    Console.WriteLine($"客户端查询返回用户信息:{Newtonsoft.Json.JsonConvert.SerializeObject(user)}");
                }
                catch (Exception ex)
                {
                    if (userServiceClient != null)
                    {
                        userServiceClient.Abort();
                    }
                    throw ex;
                }
            }
        }
    }
}
