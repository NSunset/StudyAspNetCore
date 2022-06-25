using Castle.Components.DictionaryAdapter;
using Castle.DynamicProxy;
using System;
using System.Collections.Generic;
using System.Text;

namespace Nw.AOP.DynamicProxy
{
    /// <summary>
    /// 动态实现AOP
    /// </summary>
    public class DynamicProxyEntrance
    {
        public static void Show()
        {
            //使用Castle实现动态代理
            {
                //1、安装Castle
                //2、创建拦截类UserServiceProxy，继承IInterceptor
                //3、创建ProxyGenerator 代理生成器
                //4、proxyGenerator.CreateInterfaceProxyWithTarget创建代理生成实例
                //5、根据代理生成实例调用方法
                IUserService userService = new UserService();

                UserServiceProxy userServiceProxy = new UserServiceProxy();

                ProxyGenerator proxyGenerator = new ProxyGenerator();
                IUserService proxy = proxyGenerator.CreateInterfaceProxyWithTarget(userService, userServiceProxy);

                User u = proxy.Find();

                Console.WriteLine($"u.name={u.Name}");
            }
        }
    }
}
