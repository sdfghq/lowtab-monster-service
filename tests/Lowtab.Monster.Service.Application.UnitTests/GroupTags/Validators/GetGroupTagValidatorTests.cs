using FluentAssertions;
using FluentValidation.TestHelper;
using Lowtab.Monster.Service.Application.GroupTags.Queryes;
using Lowtab.Monster.Service.Application.GroupTags.Validators;
using Xunit;

namespace Lowtab.Monster.Service.Application.UnitTests.GroupTags.Validators;

public class GetGroupTagValidatorTests
{
    private readonly GetGroupTagValidator _validator = new();

    public static IEnumerable<object[]> ValidData()
    {
        yield return [new GetGroupTagQuery { Id = Guid.NewGuid() }];
    }

    public static IEnumerable<object[]> InvalidData()
    {
        yield return [new GetGroupTagQuery { Id = Guid.Empty }];
        yield return [null!];
    }

    [Theory]
    [MemberData(nameof(ValidData))]
    public async Task Validate_ValidRequest_SuccessfulResult(GetGroupTagQuery request)
    {
        // Act
        var result = await _validator.TestValidateAsync(request);

        // Assert
        result.IsValid.Should().Be(true);
    }

    [Theory]
    [MemberData(nameof(InvalidData))]
    public async Task Validate_InvalidRequest_UnsuccessfulResult(GetGroupTagQuery request)
    {
        // Act
        var result = await _validator.TestValidateAsync(request);

        // Assert
        result.IsValid.Should().Be(false);
    }
}
