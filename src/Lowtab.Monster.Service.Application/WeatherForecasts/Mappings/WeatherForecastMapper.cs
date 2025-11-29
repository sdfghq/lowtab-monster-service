using Lowtab.Monster.Service.Application.WeatherForecasts.Commands;
using Lowtab.Monster.Service.Contracts.WeatherForecasts.GetWeatherForecast;
using Lowtab.Monster.Service.Domain.Entities;
using Riok.Mapperly.Abstractions;

namespace Lowtab.Monster.Service.Application.WeatherForecasts.Mappings;

[Mapper(UseDeepCloning = true, EnumMappingStrategy = EnumMappingStrategy.ByName)]
internal static partial class WeatherForecastMapper
{
    [MapperIgnoreTarget(nameof(WeatherForecastEntity.UpdatedAt))]
    [MapperIgnoreTarget(nameof(WeatherForecastEntity.CreatedAt))]
    [MapperIgnoreTarget(nameof(WeatherForecastEntity.Id))]
    internal static partial WeatherForecastEntity ToEntity(this CreateWeatherForecastCommand weatherForecast);

    internal static partial GetWeatherForecastResponse ToDto(this WeatherForecastEntity weatherForecast);
}
