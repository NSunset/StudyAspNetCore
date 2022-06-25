using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Nw.EFCore.CodeFirst.Context;
using Nw.EFCore.DbFirst.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Nw.WebApiCore.Controllers
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

        private readonly MyDemoContext _dbContext;
        private readonly MyDemo1TestDbContext _dbContext1;

        public WeatherForecastController(ILogger<WeatherForecastController> logger, MyDemoContext context,
            MyDemo1TestDbContext context1
            )
        {
            _logger = logger;
            _dbContext = context;
            _dbContext1 = context1;
        }

        [HttpGet]
        public IEnumerable<WeatherForecast> Get()
        {
            //dbfirst
            {
                var users = _dbContext.TbUser.AsNoTracking().ToList();

                var addtables = _dbContext.TbAddtable.AsNoTracking().ToList();
            }
            //codefirst
            {
                var users = _dbContext1.User.AsNoTracking().ToList();

                var addtables = _dbContext1.Addtable.AsNoTracking().ToList();
            }
            var rng = new Random();
            return Enumerable.Range(1, 5).Select(index => new WeatherForecast
            {
                Date = DateTime.Now.AddDays(index),
                TemperatureC = rng.Next(-20, 55),
                Summary = Summaries[rng.Next(Summaries.Length)]
            })
            .ToArray();
        }
    }
}
