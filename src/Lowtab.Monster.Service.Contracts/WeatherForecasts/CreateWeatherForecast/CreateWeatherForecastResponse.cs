using Lowtab.Monster.Service.Contracts.WeatherForecasts.Common;

namespace Lowtab.Monster.Service.Contracts.WeatherForecasts.CreateWeatherForecast;

/// <summary>
///     Ответ при создании объекта <see cref="WeatherForecastBase" />>
/// </summary>
public record CreateWeatherForecastResponse
{
    /// <summary>
    ///     Идентификатор созданного объекта
    /// </summary>
    public Guid Id { get; init; }
}
