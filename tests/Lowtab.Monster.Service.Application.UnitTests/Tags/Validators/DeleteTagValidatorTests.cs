using FluentAssertions;
using FluentValidation.TestHelper;
using Lowtab.Monster.Service.Application.Tags.Commands;
using Lowtab.Monster.Service.Application.Tags.Validators;
using Lowtab.Monster.Service.Contracts.GroupTags;
using Xunit;

namespace Lowtab.Monster.Service.Application.UnitTests.Tags.Validators;

public class DeleteTagValidatorTests
{
    private readonly DeleteTagValidator _validator = new();

    public static IEnumerable<object[]> ValidData()
    {
        yield return [new DeleteTagCommand { Id = "test-id", Group = GroupTagEnum.Map }];
    }

    public static IEnumerable<object[]> InvalidData()
    {
        yield return [new DeleteTagCommand { Id = string.Empty, Group = GroupTagEnum.Map }];
        yield return [null!];
    }

    [Theory]
    [MemberData(nameof(ValidData))]
    public async Task Validate_ValidRequest_SuccessfulResult(DeleteTagCommand request)
    {
        // Act
        var result = await _validator.TestValidateAsync(request);

        // Assert
        result.IsValid.Should().Be(true);
    }

    [Theory]
    [MemberData(nameof(InvalidData))]
    public async Task Validate_InvalidRequest_UnsuccessfulResult(DeleteTagCommand request)
    {
        // Act
        var result = await _validator.TestValidateAsync(request);

        // Assert
        result.IsValid.Should().Be(false);
    }
}
