using Lowtab.Monster.Service.Application.Common.Exceptions;
using Lowtab.Monster.Service.Application.Interfaces;
using Lowtab.Monster.Service.Application.WeatherForecasts.Queryes;
using Lowtab.Monster.Service.Contracts.WeatherForecasts.GetWeatherForecast;
using Mediator;
using Microsoft.Extensions.Logging;
using WeatherForecastMapper = Lowtab.Monster.Service.Application.WeatherForecasts.Mappings.WeatherForecastMapper;

namespace Lowtab.Monster.Service.Application.WeatherForecasts.Handlers;

internal abstract class GetWeatherForecastHandler(
    ILogger<GetWeatherForecastHandler> logger,
    IDbContext context
) : IQueryHandler<GetWeatherForecastQuery, GetWeatherForecastResponse>
{
    public virtual async ValueTask<GetWeatherForecastResponse> Handle(GetWeatherForecastQuery request,
        CancellationToken ct)
    {
        logger.LogInformation("Try getting {EntityId} from database", request.Id);
        var entity = await context.WeatherForecasts.FindAsync([request.Id], ct) ??
                     throw new NotFoundException($"Не нашел объект с идентификатором {request.Id}");

        var result = WeatherForecastMapper.ToDto(entity);
        return result;
    }
}
