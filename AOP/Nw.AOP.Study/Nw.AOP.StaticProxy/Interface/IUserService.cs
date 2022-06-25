using System;
using System.Collections.Generic;
using System.Text;

namespace Nw.AOP.StaticProxy
{
    public interface IUserService
    {
        [LogDecorator]
        User Find();


    }
}
