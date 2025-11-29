#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
namespace Lowtab.Monster.Service.Contracts.WeatherForecasts.Common;

/// <inheritdoc cref="WeatherForecastBase" />
public record WeatherForecast : WeatherForecastBase
{
    /// <summary>
    ///     Идентификатор
    /// </summary>
    public Guid Id { get; init; }
}
