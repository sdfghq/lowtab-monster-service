using FluentAssertions;
using FluentValidation.TestHelper;
using Lowtab.Monster.Service.Application.GroupTags.Commands;
using Lowtab.Monster.Service.Application.GroupTags.Validators;
using Xunit;

namespace Lowtab.Monster.Service.Application.UnitTests.GroupTags.Validators;

public class CreateGroupTagValidatorTests
{
    private readonly CreateGroupTagValidator _validator = new();

    public static IEnumerable<object[]> ValidData()
    {
        yield return [Arrange.GetValidCreateGroupTagCommand()];
    }

    public static IEnumerable<object[]> InvalidData()
    {
        yield return [null!];
    }

    [Theory]
    [MemberData(nameof(ValidData))]
    public async Task Validate_ValidRequest_SuccessfulResult(CreateGroupTagCommand request)
    {
        // Act
        var result = await _validator.TestValidateAsync(request);

        // Assert
        result.IsValid.Should().Be(true);
    }

    [Theory]
    [MemberData(nameof(InvalidData))]
    public async Task Validate_InvalidRequest_UnsuccessfulResult(CreateGroupTagCommand request)
    {
        // Act
        var result = await _validator.TestValidateAsync(request);

        // Assert
        result.IsValid.Should().Be(false);
    }
}
