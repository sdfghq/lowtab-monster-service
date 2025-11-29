using Lowtab.Monster.Service.Application.Interfaces;
using Lowtab.Monster.Service.Application.WeatherForecasts.Options;
using Lowtab.Monster.Service.Application.WeatherForecasts.Queryes;
using Lowtab.Monster.Service.Contracts.WeatherForecasts.GetWeatherForecast;
using Microsoft.Extensions.Caching.Hybrid;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace Lowtab.Monster.Service.Application.WeatherForecasts.Handlers;

internal class GetWeatherForecastWithCacheHandler(
    ILogger<GetWeatherForecastHandler> logger,
    IDbContext context,
    HybridCache cacheService,
    IOptions<WeatherForecastCacheOptions> options
) : GetWeatherForecastHandler(logger, context)
{
    public override async ValueTask<GetWeatherForecastResponse> Handle(GetWeatherForecastQuery request,
        CancellationToken ct)
    {
        var key = options.Value.CacheKey + options.Value.CacheSeparator + request.Id;
        var cacheEntryOptions = new HybridCacheEntryOptions { Expiration = options.Value.CacheLifeTime };

        var value = await cacheService.GetOrCreateAsync<GetWeatherForecastResponse>(key,
            token => base.Handle(request, token), cacheEntryOptions, cancellationToken: ct);

        return value;
    }
}
