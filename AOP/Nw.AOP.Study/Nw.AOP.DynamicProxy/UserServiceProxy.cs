using Castle.DynamicProxy;
using System;
using System.Collections.Generic;
using System.Text;

namespace Nw.AOP.DynamicProxy
{
    public class UserServiceProxy : IInterceptor
    {
        public void Intercept(IInvocation invocation)
        {
            Console.WriteLine("在方法执行前添加逻辑");


            invocation.Proceed();


            Console.WriteLine("在方法执行后添加逻辑");
        }
    }
}
