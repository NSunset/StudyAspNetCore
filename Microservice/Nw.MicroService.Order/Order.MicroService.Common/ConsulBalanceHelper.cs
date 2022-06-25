using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Consul;

namespace Order.MicroService.Common
{
    /// <summary>
    /// 服务发现，负载均衡
    /// </summary>
    public class ConsulBalanceHelper
    {
        public static async Task<AgentService> ChooseService(string serviceName)
        {
            using (ConsulClient consulClient = new ConsulClient(c => c.Address = new Uri("http://192.168.157.128:8500")))
            {
                //获取Consul所有服务
                Dictionary<string, AgentService> services = (await consulClient.Agent.Services()).Response;

                //获取指定组名的服务
                IEnumerable<AgentService> targetServices = services.Where(x => x.Value.Service.Equals(serviceName)).Select(x => x.Value);

                //随机取指定组里的一个服务
                AgentService targetService = targetServices.ElementAt(new Random().Next(1, 1000) % targetServices.Count());

                Console.WriteLine($"{DateTime.Now} 当前调用服务为：{targetService.Address}:{targetService.Port}");
                return targetService;
            }
        }
    }
}
