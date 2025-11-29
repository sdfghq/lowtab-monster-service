using FluentAssertions;
using FluentValidation.TestHelper;
using Lowtab.Monster.Service.Application.Articles.Commands;
using Lowtab.Monster.Service.Application.Articles.Validators;
using Xunit;

namespace Lowtab.Monster.Service.Application.UnitTests.Articles.Validators;

public class DeleteArticleValidatorTests
{
    private readonly DeleteArticleValidator _validator = new();

    public static IEnumerable<object[]> ValidData()
    {
        yield return [new DeleteArticleCommand { Id = Guid.NewGuid() }];
    }

    public static IEnumerable<object[]> InvalidData()
    {
        yield return [new DeleteArticleCommand { Id = Guid.Empty }];
        yield return [null!];
    }

    [Theory]
    [MemberData(nameof(ValidData))]
    public async Task Validate_ValidRequest_SuccessfulResult(DeleteArticleCommand request)
    {
        // Act
        var result = await _validator.TestValidateAsync(request);

        // Assert
        result.IsValid.Should().Be(true);
    }

    [Theory]
    [MemberData(nameof(InvalidData))]
    public async Task Validate_InvalidRequest_UnsuccessfulResult(DeleteArticleCommand request)
    {
        // Act
        var result = await _validator.TestValidateAsync(request);

        // Assert
        result.IsValid.Should().Be(false);
    }
}
