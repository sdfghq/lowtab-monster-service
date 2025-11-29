using FluentValidation;
using Lowtab.Monster.Service.Application.Common.Validators;
using Lowtab.Monster.Service.Application.WeatherForecasts.Commands;

namespace Lowtab.Monster.Service.Application.WeatherForecasts.Validators;

internal class CreateWeatherForecastValidator : NotNullRequestValidator<CreateWeatherForecastCommand>
{
    public CreateWeatherForecastValidator()
    {
        RuleFor(x => x.Date).NotNull().NotEqual(DateOnly.MinValue);
    }
}
