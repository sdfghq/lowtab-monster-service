using Lowtab.Monster.Service.Contracts.WeatherForecasts.Common;

namespace Lowtab.Monster.Service.Contracts.WeatherForecasts.GetWeatherForecast;

/// <summary>
///     Ответ на запрос получения информации об объекте <see cref="WeatherForecast" />
/// </summary>
public record GetWeatherForecastResponse : WeatherForecast
{
    /// <summary>
    ///     Дата создания
    /// </summary>
    public DateTimeOffset CreatedAt { get; init; }

    /// <summary>
    ///     Дата обновления
    /// </summary>
    public DateTimeOffset UpdatedAt { get; init; }
}
