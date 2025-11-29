using Lowtab.Monster.Service.Application.Interfaces;
using Lowtab.Monster.Service.Application.WeatherForecasts.Commands;
using Lowtab.Monster.Service.Application.WeatherForecasts.Mappings;
using Lowtab.Monster.Service.Contracts.WeatherForecasts.CreateWeatherForecast;
using Mediator;
using Microsoft.Extensions.Logging;

namespace Lowtab.Monster.Service.Application.WeatherForecasts.Handlers;

internal class CreateWeatherForecastHandler(
    ILogger<CreateWeatherForecastHandler> logger,
    IDbContext context
) : ICommandHandler<CreateWeatherForecastCommand, CreateWeatherForecastResponse>
{
    public async ValueTask<CreateWeatherForecastResponse> Handle(CreateWeatherForecastCommand request,
        CancellationToken ct)
    {
        var newEntity = request.ToEntity();
        logger.LogInformation("Try saving {@Entity} to database", newEntity);

        var result = context.WeatherForecasts.Add(newEntity).Entity;
        await context.SaveChangesAsync(ct);

        logger.LogInformation("Created new {@Object} with {Id}", result, result.Id);
        return new CreateWeatherForecastResponse { Id = result.Id };
    }
}
