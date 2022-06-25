using Castle.DynamicProxy;
using System;
using System.Collections.Generic;
using System.Text;

namespace Nw.AOP.DynamicProxy
{
    public interface IUserService
    {
       User Find();
    }
}
