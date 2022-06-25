using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Nw.MyWebSocket.Jwt;
using Nw.MyWebSocket.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Nw.MyWebSocket.Controllers
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

        private readonly IJwtService _jwtService;

        public WeatherForecastController(ILogger<WeatherForecastController> logger,
            IJwtService jwtService
            )
        {
            _logger = logger;
            _jwtService = jwtService;
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

        [HttpPost]
        [Route("Login")]
        public string Login()
        {
            UserDto user = new UserDto
            {
                Id = 10,
                Age = 26,
                Name = "张三"
            };
            string token=_jwtService.GetToken(user);

            return token;
        }
    }
}
