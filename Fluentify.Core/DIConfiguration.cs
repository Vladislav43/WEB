using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Fluentify.Core.Interfaces;
using Fluentify.Core.Services;
using Fluentify.Models.Configuration;

namespace Fluentify.Core
{
    public static class DIConfiguration
    {
        public static void RegisterCoreDependencies(this IServiceCollection services)
        {
            services.AddTransient<IWeatherForecastService, WeatherForecastService>();
        }

        public static void RegisterCoreConfiguration(this IServiceCollection services, IConfigurationRoot configuration)
        {
            services.Configure<AppConfig>(configuration.GetSection("AppConfig"));
        }
    }
}
