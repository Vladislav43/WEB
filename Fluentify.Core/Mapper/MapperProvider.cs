using AutoMapper;
using Fluentify.Models.Database;
using Fluentify.Models.Frontend;
using Fluentify.Core.Interfaces;
using Fluentify.Core.Interfaces;
using static Microsoft.ApplicationInsights.MetricDimensionNames.TelemetryContext;
using Fluentify.Models.Database.Tables;

namespace Fluentify.Core.Mapper
{
    public class MapperProvider : IMapperProvider
    {
        private readonly IMapper _mapper;

        public MapperProvider()
        {
            _mapper = Initialize();
        }

        public IMapper GetMapper() => _mapper;

        private IMapper Initialize()
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<Users, Users>()
                    .ForMember(dest => dest.Nickname, opt => opt.MapFrom(src => src.Nickname))
                    .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.Email))
                    .ForMember(dest => dest.Password, opt => opt.MapFrom(src => src.Password))
                    .ForMember(dest => dest.RegDate, opt => opt.MapFrom(src => src.RegDate));
            });

            config.AssertConfigurationIsValid();
            return config.CreateMapper();
        }
    }
}