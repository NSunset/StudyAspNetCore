using EventBus.Abstractions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Nw.LiveBackgroundManagement.Common.Api;
using Nw.LiveBackgroundManagement.Models.EventMsg;
using RedisHelper.Interface;
using RedisHelper.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Nw.LiveReceptionManage.WebApi.Controllers
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
        private IEventBus _eventBus;
        private RedisStringService _redisString;
        private IRedisConfigureHelper _redisHelper;

        public WeatherForecastController(
            ILogger<WeatherForecastController> logger,
            IEventBus eventBus,
            RedisStringService redisString,
            IRedisConfigureHelper redisHelper
            )
        {
            _logger = logger;
            _eventBus = eventBus;
            _redisString = redisString;
            _redisHelper = redisHelper;
        }

        [HttpGet]
        public IEnumerable<WeatherForecast> Get()
        {

            _logger.LogTrace("你好啊");
            _logger.LogDebug("你好啊");
            _logger.LogInformation("你好啊");
            _logger.LogWarning("你好啊");
            _logger.LogError("你好啊");


            string key = "haxi001";

            _redisString.Set(key, "你好啊", TimeSpan.FromHours(2));

            string value = _redisString.Get(key);

            _redisString.SetConnection(_redisHelper.GetRedisConnectionString(), 1);


            _redisString.Set(key, "我不好啊", TimeSpan.FromHours(2));

            string value1 = _redisString.Get(key);


            var rng = new Random();
            return Enumerable.Range(1, 5).Select(index => new WeatherForecast
            {
                Date = DateTime.Now.AddDays(index),
                TemperatureC = rng.Next(-20, 55),
                Summary = Summaries[rng.Next(Summaries.Length)]
            })
            .ToArray();
        }

        [Route("OrderAdd")]
        [HttpGet]
        public IActionResult TestOrderAdd(string userId)
        {
            OrderStartedIntegrationEvent orderIntegrationEvent = new OrderStartedIntegrationEvent(userId);
            _eventBus.Publish(orderIntegrationEvent);

            return new JsonResult(ApiResult.Ok());
        }

        [Route("ProductAdd")]
        [HttpGet]
        public IActionResult TestProductAdd(int productId, decimal newPrice, decimal oldPrice)
        {
            ProductPriceChangedIntegrationEvent productIntegrationEvent = new ProductPriceChangedIntegrationEvent(productId, newPrice, oldPrice);
            _eventBus.Publish(productIntegrationEvent);

            return new JsonResult(ApiResult.Ok());
        }


    }
}
