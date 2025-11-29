namespace Lowtab.Monster.Service.Contracts.WeatherForecasts.Common;

/// <summary>
///     Объект с прогнозом погоды
/// </summary>
public record WeatherForecastBase
{
    /// <summary>
    ///     Дата
    /// </summary>
    public DateOnly Date { get; init; }

    /// <summary>
    ///     Температура по Цельсию
    /// </summary>
    public int TemperatureC { get; init; }

    /// <summary>
    ///     Дополнительная информация
    /// </summary>
    public string? Summary { get; init; }
}
