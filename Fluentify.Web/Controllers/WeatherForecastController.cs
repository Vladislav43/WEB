using Microsoft.AspNetCore.Mvc;
using Fluentify.Core.Interfaces;
using Fluentify.Models.Weather;

namespace Fluentify.API.Controllers
{
    [ApiController]
    [Route("/api/[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        private readonly IWeatherForecastService _forecastService;

        public WeatherForecastController(IWeatherForecastService forecastService)
        {
            _forecastService = forecastService;
        }

        [HttpGet]
        [Route("Get")]
        public IEnumerable<WeatherForecast> Get()
        {
            return _forecastService.GetRandomForecast();
        }
    }
}