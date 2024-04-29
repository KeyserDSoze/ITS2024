using Microsoft.AspNetCore.Mvc;

namespace Amazon.Payment.Api.Controllers
{
    [ApiController]
    [Route("v1/[controller]/[action]")]
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
        public IEnumerable<WeatherForecast> List()
        {
            return Enumerable.Range(1, 5).Select(index => new WeatherForecast
            {
                TemperatureC = Random.Shared.Next(-20, 55),
                Summary = Summaries[Random.Shared.Next(Summaries.Length)]
            })
            .ToArray();
        }
        [HttpPost]
        public bool Add(WeatherForecast weatherForecast)
        {
            // Add weather forecast
            return true;
        }
        [HttpDelete]
        public bool Delete(int id)
        {
            // Delete weather forecast
            return true;
        }
    }
}
