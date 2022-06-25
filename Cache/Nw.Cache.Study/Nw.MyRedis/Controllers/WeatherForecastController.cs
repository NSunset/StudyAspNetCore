using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Nw.Cache.Redis.Interface;
using Nw.Cache.Redis.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Nw.MyRedis.Controllers
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
        private RedisStringService _redisStringService;

        public WeatherForecastController(ILogger<WeatherForecastController> logger,
            RedisStringService redisStringService
            )
        {
            _logger = logger;
            _redisStringService = redisStringService;
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

        [Route("GetRedis")]
        [HttpGet]
        public string GetRedis()
        {
            //RedisStringService redisString = RedisFactory.GetDefaultRedis(RedisDataType.DbString);

            string key = "nw_13";
            string result = _redisStringService.Get(key);
            if (string.IsNullOrWhiteSpace(result))
            {
                result = "26";
                _redisStringService.Set(key, result);
            }
            return result;
        }
    }
}
