using AutoMapper;
using Fluentify.Core.Interfaces;
using Fluentify.Core.Mapper;
using Fluentify.Models.Configuration;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Fluentify.Core.Interfaces;
using Fluentify.Core.Mapper;
using Fluentify.Models.Configuration;

namespace Fluentify.Core
{
    public static class DIConfiguration
    {
        public static void RegisterCoreDependencies(this IServiceCollection services)
        {
            services.AddSingleton<IMapperProvider, MapperProvider>();
            services.AddSingleton(GetMapper);
        }

        private static IMapper GetMapper(IServiceProvider serviceProvider)
        {
            var provider = serviceProvider.GetRequiredService<IMapperProvider>();
            return provider.GetMapper();
        }

        public static void RegisterCoreConfiguration(this IServiceCollection services, IConfigurationRoot configuration)
        {
            services.Configure<AppConfig>(configuration.GetSection("AppConfig"));
        }
    }
}