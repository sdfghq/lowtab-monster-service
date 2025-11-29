using FluentAssertions;
using FluentValidation.TestHelper;
using Lowtab.Monster.Service.Application.Articles.Queryes;
using Lowtab.Monster.Service.Application.Articles.Validators;
using Xunit;

namespace Lowtab.Monster.Service.Application.UnitTests.Articles.Validators;

public class GetArticleValidatorTests
{
    private readonly GetArticleValidator _validator = new();

    public static IEnumerable<object[]> ValidData()
    {
        yield return [new GetArticleQuery { Id = Guid.NewGuid() }];
    }

    public static IEnumerable<object[]> InvalidData()
    {
        yield return [new GetArticleQuery { Id = Guid.Empty }];
        yield return [null!];
    }

    [Theory]
    [MemberData(nameof(ValidData))]
    public async Task Validate_ValidRequest_SuccessfulResult(GetArticleQuery request)
    {
        // Act
        var result = await _validator.TestValidateAsync(request);

        // Assert
        result.IsValid.Should().Be(true);
    }

    [Theory]
    [MemberData(nameof(InvalidData))]
    public async Task Validate_InvalidRequest_UnsuccessfulResult(GetArticleQuery request)
    {
        // Act
        var result = await _validator.TestValidateAsync(request);

        // Assert
        result.IsValid.Should().Be(false);
    }
}
