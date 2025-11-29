using FluentAssertions;
using FluentValidation.TestHelper;
using Lowtab.Monster.Service.Application.Tags.Commands;
using Lowtab.Monster.Service.Application.Tags.Validators;
using Xunit;

namespace Lowtab.Monster.Service.Application.UnitTests.Tags.Validators;

public class CreateTagValidatorTests
{
    private readonly CreateTagValidator _validator = new();

    public static IEnumerable<object[]> ValidData()
    {
        yield return [Arrange.GetValidCreateTagCommand()];
    }

    public static IEnumerable<object[]> InvalidData()
    {
        yield return [null!];
    }

    [Theory]
    [MemberData(nameof(ValidData))]
    public async Task Validate_ValidRequest_SuccessfulResult(CreateTagCommand request)
    {
        // Act
        var result = await _validator.TestValidateAsync(request);

        // Assert
        result.IsValid.Should().Be(true);
    }

    [Theory]
    [MemberData(nameof(InvalidData))]
    public async Task Validate_InvalidRequest_UnsuccessfulResult(CreateTagCommand request)
    {
        // Act
        var result = await _validator.TestValidateAsync(request);

        // Assert
        result.IsValid.Should().Be(false);
    }
}
