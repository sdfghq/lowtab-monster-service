using FluentValidation;
using Lowtab.Monster.Service.Application.Common.Validators;
using Lowtab.Monster.Service.Application.WeatherForecasts.Queryes;

namespace Lowtab.Monster.Service.Application.WeatherForecasts.Validators;

internal class GetWeatherForecastValidator : NotNullRequestValidator<GetWeatherForecastQuery>
{
    public GetWeatherForecastValidator()
    {
        RuleFor(x => x.Id).NotNull().NotEmpty();
    }
}
