using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Nw.RabbitMQ.Tool;
using Nw.RabbitMQ.Tool.Init;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Nw.RabbitMQ.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        private static readonly string[] Summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        private readonly ILogger<WeatherForecastController> _logger;

        public WeatherForecastController(ILogger<WeatherForecastController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        public IEnumerable<WeatherForecast> Get()
        {
            var rng = new Random();
            return Enumerable.Range(1, 5).Select(index => new WeatherForecast
            {
                Date = DateTime.Now.AddDays(index),
                TemperatureC = rng.Next(-20, 55),
                Summary = Summaries[rng.Next(Summaries.Length)]
            })
            .ToArray();
        }

        [Route("RabbitMQTest")]
        [HttpGet]
        public async Task RabbitMQTest()
        {
            //集群测试
            List<string> hostNames = new List<string>
                {
                   "192.168.157.130",
                   "192.168.157.128",
                   "192.168.157.131",
                };
            foreach (var item in hostNames)
            {
                RabbitMQConnectionConfig config = new RabbitMQConnectionConfig
                {
                    HostName = item,
                    VirtualHost = "my_vhost",
                    UserName = "admin",
                    Password = "admin"
                };
                DefaultRabbitMQHandler rabbitMQHandler = new DefaultRabbitMQHandler(config);

                PublishMsg model = new PublishMsg(
                "哈哈哈",
                $"test{config.HostName}",
                "textEx"
                );

                rabbitMQHandler.Publish(model);
                Console.WriteLine($"生产者发送消息：{model.Message}");

                //一般消费端会寄宿到后台程序,不会在前台调用。会卡界面
                Task task = rabbitMQHandler.RegistReciveActionAsync("", m =>
                 {
                     Console.WriteLine($"消费者处理消息：{m}");
                     return true;
                 }, true);

                task.Wait();
            }
        }
    }
}
