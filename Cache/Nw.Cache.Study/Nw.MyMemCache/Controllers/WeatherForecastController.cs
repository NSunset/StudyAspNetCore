using Enyim.Caching;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Nw.MyMemCache.Controllers
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

        //注入Memcached客户端
        private readonly IMemcachedClient _memcachedClient;

        public WeatherForecastController(ILogger<WeatherForecastController> logger,
            IMemcachedClient memcachedClient)
        {
            _logger = logger;
            _memcachedClient = memcachedClient;
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


        /// <summary>
        /// 测试使用Memcached
        /// </summary>
        /// <returns></returns>
        [Route("MemcachedTest")]
        [HttpGet]
        public Task<object> MemcachedTest()
        {
            string key = "nw";
            object obj = _memcachedClient.Get(key);
            if (obj == null)
            {
                obj = $"nw_{DateTime.Now}";
                _memcachedClient.Set(key, obj, 100);
            }
            return Task.FromResult(obj);
        }
    }
}
