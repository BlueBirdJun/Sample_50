using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Sample_50.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Sample_50.Controllers
{
    [ApiController]
    [Route("[controller]")]
    [AddHeader("ApiVersion", "1.0")]
    public class CommonController : ControllerBase
    {
        private static readonly string[] Summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        private readonly ILogger<CommonController> _logger;
        private readonly WebClientService client;

        public CommonController(ILogger<CommonController> logger, WebClientService client)
        {
            _logger = logger;
            this.client = client;
        }

        [HttpGet]
        [Route("{city}")]
        public async Task<WeatherForecast> Gett1(string city)
        {
            _logger.LogInformation("경고");
            _logger.LogDebug("디버그");
            city = "london";
            var forecast = await client.GetCurrentWeatherAsync(city);
            return new WeatherForecast
            {
                Summary = forecast.weather[0].description,
                TemperatureC = (int)forecast.main.temp,
                desc1 = forecast.main.temp_min.ToString(),
                Date =DateTimeOffset.FromUnixTimeSeconds(forecast.dt).Date
            }; 
        }
        [HttpGet]
        public IEnumerable<WeatherForecast> Get()
        {
            _logger.LogInformation("안녕");
            _logger.LogDebug("실행");
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
