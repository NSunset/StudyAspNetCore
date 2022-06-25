using System;
using System.Collections.Generic;
using System.Text;

namespace Nw.AOP.StaticProxy
{
    public class LoginUserServiceDecorator : BaseUserServiceDecorator
    {
        public LoginUserServiceDecorator(IUserService userService) : base(userService)
        {
        }


        public override User Find()
        {
            Console.WriteLine("在执行IUserService方法前执行一些逻辑");

            User u = base.Find();

            Console.WriteLine("在执行IUserService方法后执行一些逻辑");

            return u;
        }
    }
}
