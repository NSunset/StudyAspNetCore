using SuperSocket.SocketEngine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nw.MySocket
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                var bootstrap = BootstrapFactory.CreateBootstrap();

                if (!bootstrap.Initialize())
                {
                    Console.WriteLine("初始化失败!");
                    return;
                }
                var result = bootstrap.Start();

                foreach (var item in bootstrap.AppServers)
                {
                    if (item.State==SuperSocket.SocketBase.ServerState.Running)
                    {
                        Console.WriteLine($"{item.Name}服务启动成功");
                    }
                    else
                    {
                        Console.WriteLine($"{item.Name}启动失败了");
                    }
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
