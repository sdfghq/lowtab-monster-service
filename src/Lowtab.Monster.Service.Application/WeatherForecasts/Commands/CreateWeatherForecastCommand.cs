using Lowtab.Monster.Service.Contracts.WeatherForecasts.CreateWeatherForecast;
using Mediator;

namespace Lowtab.Monster.Service.Application.WeatherForecasts.Commands;

/// <inheritdoc cref="CreateWeatherForecastRequest" />
public record CreateWeatherForecastCommand : CreateWeatherForecastRequest, ICommand<CreateWeatherForecastResponse>;
