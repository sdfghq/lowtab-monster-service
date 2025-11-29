using Bogus;
using Lowtab.Monster.Service.Application.WeatherForecasts.Commands;

namespace Lowtab.Monster.Service.Application.UnitTests.WeatherForecasts;

public static class Arrange
{
    private static readonly Faker<CreateWeatherForecastCommand> CreateReceiptCommandFaker =
        new Faker<CreateWeatherForecastCommand>()
            .RuleFor(x => x.Date, _ => DateOnly.FromDateTime(DateTime.UtcNow))
            .RuleFor(x => x.TemperatureC, f => f.Random.Int(-50, 65));

    public static CreateWeatherForecastCommand GetValidCreateWeatherForecastCommand()
    {
        return CreateReceiptCommandFaker.Generate();
    }
}
