using Bogus;
using Lowtab.Monster.Service.Application.Articles.Commands;
using Lowtab.Monster.Service.Application.Articles.Mappings;
using Lowtab.Monster.Service.Contracts.Articles.GetArticle;
using Lowtab.Monster.Service.Domain.Entities;

namespace Lowtab.Monster.Service.Application.UnitTests.Articles;

public static class Arrange
{
    private static readonly Faker<CreateArticleCommand> CreateReceiptCommandFaker = new Faker<CreateArticleCommand>()
        .RuleFor(x => x.Title, f => f.Commerce.ProductName())
        .RuleFor(x => x.Body, f => f.Lorem.Paragraph());

    private static readonly Faker<UpdateArticleCommand> UpdateReceiptCommandFaker = new Faker<UpdateArticleCommand>()
        .RuleFor(x => x.Title, f => f.Commerce.ProductName())
        .RuleFor(x => x.Body, f => f.Lorem.Paragraph())
        .RuleFor(x => x.Id, f => f.Random.Guid());

    private static readonly Faker<GetArticleResponse> GetReceiptBaseResponseFaker = new Faker<GetArticleResponse>()
        .RuleFor(x => x.Id, f => f.Random.Guid())
        .RuleFor(x => x.Title, f => f.Commerce.ProductName())
        .RuleFor(x => x.Body, f => f.Lorem.Paragraph());

    public static CreateArticleCommand GetValidCreateArticleCommand()
    {
        return CreateReceiptCommandFaker.Generate();
    }

    public static UpdateArticleCommand GetValidUpdateArticleCommand()
    {
        return UpdateReceiptCommandFaker.Generate();
    }

    public static GetArticleResponse GetValidGetArticleResponse()
    {
        return GetReceiptBaseResponseFaker.Generate();
    }

    public static ArticleEntity GenerateArticleEntity()
    {
        return CreateReceiptCommandFaker.Generate().ToEntity();
    }
}
