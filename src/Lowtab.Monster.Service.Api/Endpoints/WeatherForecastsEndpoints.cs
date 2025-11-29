using System.Net;
using Lowtab.Monster.Service.Application.WeatherForecasts.Commands;
using Lowtab.Monster.Service.Application.WeatherForecasts.Queryes;
using Lowtab.Monster.Service.Contracts.WeatherForecasts;
using Lowtab.Monster.Service.Contracts.WeatherForecasts.CreateWeatherForecast;
using Lowtab.Monster.Service.Contracts.WeatherForecasts.GetWeatherForecast;
using Mediator;
using Microsoft.AspNetCore.Mvc;

namespace Lowtab.Monster.Service.Api.Endpoints;

internal static class WeatherForecastsEndpoints
{
    private const string Tag = "WeatherForecast internal methods";

    public static WebApplication MapWeatherForecastEndpoints(this WebApplication app)
    {
        var api = app
            .NewVersionedApi(Tag)
            .HasApiVersion(1.0);

        api.MapPost(WeatherForecastsRoutes.CreateWeatherForecast, CreateWeatherForecast);
        api.MapGet(WeatherForecastsRoutes.GetWeatherForecast, GetWeatherForecast);

        return app;
    }

    /// <summary>
    ///     Создать новый объект
    /// </summary>
    /// <param name="request"></param>
    /// <param name="mediator"></param>
    /// <param name="ct"></param>
    /// <returns></returns>
    [ProducesResponseType(typeof(ProblemDetails), (int)HttpStatusCode.BadRequest)]
    [ProducesResponseType(typeof(ProblemDetails), (int)HttpStatusCode.InternalServerError)]
    [ProducesResponseType(typeof(CreateWeatherForecastResponse), (int)HttpStatusCode.OK)]
    private static async Task<CreateWeatherForecastResponse> CreateWeatherForecast(
        [FromBody] CreateWeatherForecastRequest request,
        [FromServices] ISender mediator, CancellationToken ct = default)
    {
        var result =
            await mediator.Send(
                new CreateWeatherForecastCommand
                {
                    Date = request.Date, TemperatureC = request.TemperatureC, Summary = request.Summary
                }, ct);
        return result;
    }

    /// <summary>
    ///     Получить объект по идентификатору
    /// </summary>
    /// <param name="id"></param>
    /// <param name="mediator"></param>
    /// <param name="ct"></param>
    /// <returns></returns>
    [ProducesResponseType(typeof(ProblemDetails), (int)HttpStatusCode.BadRequest)]
    [ProducesResponseType(typeof(ProblemDetails), (int)HttpStatusCode.NotFound)]
    [ProducesResponseType(typeof(ProblemDetails), (int)HttpStatusCode.InternalServerError)]
    [ProducesResponseType(typeof(CreateWeatherForecastResponse), (int)HttpStatusCode.OK)]
    private static async Task<GetWeatherForecastResponse> GetWeatherForecast([FromRoute] Guid id,
        [FromServices] ISender mediator,
        CancellationToken ct = default)
    {
        var result = await mediator.Send(new GetWeatherForecastQuery { Id = id }, ct);
        return result;
    }
}
