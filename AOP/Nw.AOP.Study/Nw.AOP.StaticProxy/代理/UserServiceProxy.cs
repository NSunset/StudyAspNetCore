using System;
using System.Collections.Generic;
using System.Text;

namespace Nw.AOP.StaticProxy
{
    public class UserServiceProxy : IUserService
    {
        private IUserService _userService;
        public UserServiceProxy(IUserService userService)
        {
            _userService = userService;
        }


        public User Find()
        {
            Console.WriteLine("在执行IUserService方法前执行业务逻辑");
            User u = _userService.Find();

            Console.WriteLine("在执行IUserService方法后执行业务逻辑");
            return u;
        }
    }
}
