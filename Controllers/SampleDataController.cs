using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ColaTerminal.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ColaTerminal.Controllers
{
    [Route("api/[controller]")]
    public class SampleDataController : Controller
    {

        private readonly traperto_kurtContext dbcontext;

        public SampleDataController(traperto_kurtContext dbcontext)
        {
            this.dbcontext = dbcontext;
        }

        private static string[] Summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        [HttpGet("[action]")]
        public IEnumerable<WeatherForecast> WeatherForecasts()
        {
            var rng = new Random();
            return Enumerable.Range(1, 5).Select(index => new WeatherForecast
            {
                DateFormatted = DateTime.Now.AddDays(index).ToString("d"),
                TemperatureC = rng.Next(-20, 55),
                Summary = Summaries[rng.Next(Summaries.Length)]
            });
        }

        [HttpGet("[action]")]
        public User helloWorld()
        {
            return dbcontext.User.Include(x => x.Proceed).ThenInclude(y => y.Drink).First();
        }

        [HttpGet("[action]")]
        public Drink drinks()
        {
            return dbcontext.Drink.Include(x => x.Proceed).First();
        }

        public class WeatherForecast
        {
            public string DateFormatted { get; set; }
            public int TemperatureC { get; set; }
            public string Summary { get; set; }

            public int TemperatureF
            {
                get { return 32 + (int)(TemperatureC / 0.5556); }
            }
        }
    }
}