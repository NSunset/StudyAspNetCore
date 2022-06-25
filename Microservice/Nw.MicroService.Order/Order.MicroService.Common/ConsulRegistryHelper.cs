using Consul;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Order.MicroService.Common
{
    public static class ConsulRegistryHelper
    {
        public static void ConsulRegistry(this IConfiguration configuration)
        {
            try
            {
                string ip = configuration["Consul:ip"];
                string port = configuration["Consul:port"];
                string weight = configuration["Consul:weight"];
                string consulAddress = configuration["Consul:ConsulAddress"];
                string consulCenter = configuration["Consul:ConsulCenter"];

                ConsulClient client = new ConsulClient(c =>
                {
                    c.Address = new Uri(consulAddress);
                    c.Datacenter = consulCenter;
                });

                client.Agent.ServiceRegister(new AgentServiceRegistration()
                {
                    ID = "OrderService " + Guid.NewGuid(),//--唯一的
                    Name = "OrderService",//分组---根据Service
                    Address = ip,
                    Port = int.Parse(port),
                    Tags = new string[] { weight.ToString() },//额外标签信息
                    Check = new AgentServiceCheck() // 添加心跳检查
                    {
                        Interval = TimeSpan.FromSeconds(12),
                        HTTP = $"http://{ip}:{port}/Api/Health/Index",
                        Timeout = TimeSpan.FromSeconds(5),
                        DeregisterCriticalServiceAfter = TimeSpan.FromSeconds(20)
                    }//配置心跳
                });
                Console.WriteLine($"{ip}:{port}--weight:{weight}"); //命令行参数获取
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Consul注册：{ex.Message}");
            }
        }
    }
}
