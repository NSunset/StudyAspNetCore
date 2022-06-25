using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace Nw.AOP.StaticProxy
{
    /// <summary>
    /// 静态实现AOP
    /// </summary>
    public class StaticProxyEntrance
    {
        public static void Show()
        {
            //装饰器
            {
                //IUserService userService = new UserService();

                //userService = new LoginUserServiceDecorator(userService);

                //User u = userService.Find();

                //Console.WriteLine($"u.name={u.Name}");
            }

            //代理
            {
                //IUserService userService = new UserService();

                //userService = new UserServiceProxy(userService);

                //User u = userService.Find();

                //Console.WriteLine($"u.name={u.Name}");
            }

            //特性加反射加委托的嵌套实现
            {
                //IUserService userService = new UserService();

                //userService.Show(nameof(IUserService.Find), null);

                //User u = userService.Show<User>(nameof(IUserService.Find), null);

                //Console.WriteLine($"u.name={u.Name}");
            }
        }
    }
}
