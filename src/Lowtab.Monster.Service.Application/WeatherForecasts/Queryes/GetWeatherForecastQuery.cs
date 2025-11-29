using Lowtab.Monster.Service.Contracts.WeatherForecasts.Common;
using Lowtab.Monster.Service.Contracts.WeatherForecasts.GetWeatherForecast;
using Mediator;

namespace Lowtab.Monster.Service.Application.WeatherForecasts.Queryes;

/// <summary>
///     Запрос для получения объекта <see cref="WeatherForecastBase" />
/// </summary>
public record GetWeatherForecastQuery : IQuery<GetWeatherForecastResponse>
{
    public required Guid Id { get; init; }
}
