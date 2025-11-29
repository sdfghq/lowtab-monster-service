using FluentAssertions;
using FluentValidation.TestHelper;
using Lowtab.Monster.Service.Application.WeatherForecasts.Commands;
using Lowtab.Monster.Service.Application.WeatherForecasts.Validators;
using Xunit;

namespace Lowtab.Monster.Service.Application.UnitTests.WeatherForecasts.Validators;

public class CreateWeatherForecastValidatorTests
{
    private readonly CreateWeatherForecastValidator _validator = new();

    public static IEnumerable<object[]> ValidData()
    {
        yield return
        [
            Arrange.GetValidCreateWeatherForecastCommand()
        ];
    }

    public static IEnumerable<object[]> InvalidData()
    {
        yield return [null!];
    }

    [Theory]
    [MemberData(nameof(ValidData))]
    public async Task Validate_ValidRequest_SuccessfulResult(CreateWeatherForecastCommand request)
    {
        // Act
        var result = await _validator.TestValidateAsync(request);

        // Assert
        result.IsValid.Should().Be(true);
    }

    [Theory]
    [MemberData(nameof(InvalidData))]
    public async Task Validate_InvalidRequest_UnsuccessfulResult(CreateWeatherForecastCommand request)
    {
        // Act
        var result = await _validator.TestValidateAsync(request);

        // Assert
        result.IsValid.Should().Be(false);
    }
}
