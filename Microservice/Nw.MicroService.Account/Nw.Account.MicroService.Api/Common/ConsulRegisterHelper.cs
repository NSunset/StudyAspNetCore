using Consul;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Nw.Account.MicroService.Api.Common
{
    public static class ConsulRegisterHelper
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

                ConsulClient consulClient = new ConsulClient(c =>
                {
                    c.Address = new Uri(consulAddress);
                    c.Datacenter = consulCenter;
                });

                consulClient.Agent.ServiceRegister(new AgentServiceRegistration
                {
                    //服务地址
                    Address = ip,
                    //服务端口
                    Port = int.Parse(port),
                    //标签
                    Tags = new string[] { weight.ToString() },
                    //服务唯一ID
                    ID = $"AccountService{Guid.NewGuid()}",
                    //服务组，指定组名为AccountService
                    Name = "AccountService",
                    //心跳检查
                    Check = new AgentServiceCheck
                    {
                        //心跳地址
                        HTTP = $"http://{ip}:{port}/api/Health/Index",
                        //间隔12秒发一次心跳检查
                        Interval = TimeSpan.FromSeconds(12),
                        //超过5秒没有响应，就上报不健康
                        Timeout = TimeSpan.FromSeconds(5),
                        //超过20秒没有响应，就把服务从Consul剔除
                        DeregisterCriticalServiceAfter = TimeSpan.FromSeconds(20)
                    }
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
