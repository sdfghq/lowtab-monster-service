using FluentAssertions;
using FluentValidation.TestHelper;
using Lowtab.Monster.Service.Application.WeatherForecasts.Queryes;
using Lowtab.Monster.Service.Application.WeatherForecasts.Validators;
using Xunit;

namespace Lowtab.Monster.Service.Application.UnitTests.WeatherForecasts.Validators;

public class GetWeatherForecastValidatorTests
{
    private readonly GetWeatherForecastValidator _validator = new();

    public static IEnumerable<object[]> ValidData()
    {
        yield return
        [
            new GetWeatherForecastQuery { Id = Guid.NewGuid() }
        ];
    }

    public static IEnumerable<object[]> InvalidData()
    {
        yield return [null!];
    }

    [Theory]
    [MemberData(nameof(ValidData))]
    public async Task Validate_ValidRequest_SuccessfulResult(GetWeatherForecastQuery request)
    {
        // Act
        var result = await _validator.TestValidateAsync(request);

        // Assert
        result.IsValid.Should().Be(true);
    }

    [Theory]
    [MemberData(nameof(InvalidData))]
    public async Task Validate_InvalidRequest_UnsuccessfulResult(GetWeatherForecastQuery request)
    {
        // Act
        var result = await _validator.TestValidateAsync(request);

        // Assert
        result.IsValid.Should().Be(false);
    }
}
