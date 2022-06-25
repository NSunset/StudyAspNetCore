using Nw.AOP.DynamicProxy;
using Nw.AOP.StaticProxy;
using System;

namespace Nw.MyAOP
{
    class Program
    {
        static void Main(string[] args)
        {

            try
            {
                //AOP：在不修改原有代码情况下添加新的逻辑功能。横向扩展

                //静态实现
                {
                    //StaticProxyEntrance.Show();
                }

                //动态实现,使用castle
                {
                    DynamicProxyEntrance.Show();
                }


                Console.ReadLine();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
