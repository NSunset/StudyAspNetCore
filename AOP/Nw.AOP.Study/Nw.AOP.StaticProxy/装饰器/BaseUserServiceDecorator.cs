using System;
using System.Collections.Generic;
using System.Text;

namespace Nw.AOP.StaticProxy
{
    /// <summary>
    /// 装饰IUserService的基类
    /// </summary>
    public abstract class BaseUserServiceDecorator : IUserService
    {
        private IUserService _userService = null;
        public BaseUserServiceDecorator(IUserService userService)
        {
            _userService = userService;
        }

        public virtual User Find()
        {
            return _userService.Find();
        }
    }
}
