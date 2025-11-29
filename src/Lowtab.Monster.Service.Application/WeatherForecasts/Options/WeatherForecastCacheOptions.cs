namespace Lowtab.Monster.Service.Application.WeatherForecasts.Options;

public class WeatherForecastCacheOptions
{
    public string CacheKey { get; set; } = "WeatherForecastCache";

    public char CacheSeparator { get; set; } = ':';

    public TimeSpan CacheLifeTime { get; set; } = TimeSpan.FromSeconds(604800);
}
