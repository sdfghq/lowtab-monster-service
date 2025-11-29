using Lowtab.Monster.Service.Contracts.WeatherForecasts.Common;
using Sdf.Platform.Versioning.Contracts;

namespace Lowtab.Monster.Service.Contracts.WeatherForecasts;

/// <summary>
///     Маршруты для работы с объектами <see cref="WeatherForecast" />
/// </summary>
public static class WeatherForecastsRoutes
{
    /// <summary>
    ///     Базовый URL для работы с объектами <see cref="WeatherForecast" />
    /// </summary>
    public const string BaseUrl =
        $"/internal/v{ApiVersionParameter.RouteParameter}/weatherforecast";

    /// <summary>
    ///     Маршрут для создания объекта <see cref="WeatherForecastBase" />
    /// </summary>
    public const string CreateWeatherForecast = BaseUrl;

    /// <summary>
    ///     Маршрут для получения объекта <see cref="WeatherForecastBase" />
    /// </summary>
    public const string GetWeatherForecast = $"{BaseUrl}/{{id}}";
}
