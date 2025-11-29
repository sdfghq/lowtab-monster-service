using FluentAssertions;
using FluentValidation.TestHelper;
using Lowtab.Monster.Service.Application.Tags.Commands;
using Lowtab.Monster.Service.Application.Tags.Validators;
using Xunit;

namespace Lowtab.Monster.Service.Application.UnitTests.Tags.Validators;

public class UpdateTagValidatorTests
{
    private readonly UpdateTagValidator _validator = new();

    public static IEnumerable<object[]> ValidData()
    {
        yield return [Arrange.GetValidUpdateTagCommand()];
    }

    public static IEnumerable<object[]> InvalidData()
    {
        yield return [null!];
    }

    [Theory]
    [MemberData(nameof(ValidData))]
    public async Task Validate_ValidRequest_SuccessfulResult(UpdateTagCommand request)
    {
        // Act
        var result = await _validator.TestValidateAsync(request);

        // Assert
        result.IsValid.Should().Be(true);
    }

    [Theory]
    [MemberData(nameof(InvalidData))]
    public async Task Validate_InvalidRequest_UnsuccessfulResult(UpdateTagCommand request)
    {
        // Act
        var result = await _validator.TestValidateAsync(request);

        // Assert
        result.IsValid.Should().Be(false);
    }
}
