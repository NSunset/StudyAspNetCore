using System;
using System.Collections.Generic;
using System.Text;

namespace Nw.AOP.StaticProxy
{
    public class UserService : IUserService
    {
        public User Find()
        {
            Console.WriteLine("获取用户信息");
            return new User
            {
                Id = 100,
                Name = "张三"
            };
        }
    }
}
