using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Consul;

namespace Order.MicroServie.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ConsulController : ControllerBase
    {
        [Route("Index")]
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            string msg = string.Empty;
            //获取Consul服务客户端
            using (ConsulClient consulClient = new ConsulClient(c => c.Address = new Uri("http://192.168.157.128:8500")))
            {
                //获取Consul中所有服务实例
                Dictionary<string, AgentService> services = (await consulClient.Agent.Services()).Response;

                //找出目标服务
                IEnumerable<AgentService> tarageServices = services.Where(s => s.Value.Service.Equals("StorageService")).Select(s => s.Value);

                //实现负载均衡
                AgentService targetService = tarageServices.ElementAt(new Random().Next(1, 1000) % tarageServices.Count());

                msg = $"{DateTime.Now} 当前调用服务为：{targetService.Address}:{targetService.Port}";
                return new JsonResult(new
                {
                    msg
                });

            }
        }
    }
}
