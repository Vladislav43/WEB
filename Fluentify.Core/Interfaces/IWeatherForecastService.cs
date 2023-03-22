using Fluentify.Models.Weather;

namespace Fluentify.Core.Interfaces
{
    public interface IWeatherForecastService
    {
        IEnumerable<WeatherForecast> GetRandomForecast();
    }
}
