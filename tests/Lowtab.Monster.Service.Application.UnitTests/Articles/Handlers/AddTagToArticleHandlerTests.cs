using FluentAssertions;
using Lowtab.Monster.Service.Application.Articles.Commands;
using Lowtab.Monster.Service.Application.Articles.Handlers;
using Lowtab.Monster.Service.Domain.Entities;
using Lowtab.Monster.Service.Infrastructure.Persistence;
using Xunit;

namespace Lowtab.Monster.Service.Application.UnitTests.Articles.Handlers;

public sealed class AddTagToArticleHandlerTests : IDisposable, IAsyncDisposable
{
    private static readonly InternalDbContextFactory Factory = new();
    private readonly InternalDbContext _context = Factory.CreateDbContext([nameof(AddTagToArticleHandlerTests)]);

    public AddTagToArticleHandlerTests()
    {
        SeedData(_context).GetAwaiter().GetResult();
        ExistingArticle = _context.Articles.First();
        ExistingTag = _context.Tags.First();
    }

    private ArticleEntity ExistingArticle { get; }
    private TagEntity ExistingTag { get; }

    public ValueTask DisposeAsync()
    {
        return _context.DisposeAsync();
    }

    public void Dispose()
    {
        _context.Dispose();
    }

    private static async Task SeedData(InternalDbContext context)
    {
        var article = Arrange.GenerateArticleEntity();
        var tag = Tags.Arrange.GenerateTagEntity();

        await context.Articles.AddAsync(article);
        await context.Tags.AddAsync(tag);
        await context.SaveChangesAsync(CancellationToken.None);
    }

    [Fact]
    public async Task AddTagToArticleHandler_Handle_Successful()
    {
        // Arrange
        var handler = new AddTagToArticleHandler(_context);
        var request = new AddTagToArticleCommand { ArticleId = ExistingArticle.Id, TagId = ExistingTag.Id };

        // Act
        var result = await handler.Handle(request, CancellationToken.None);

        // Assert
        result.Should().NotBeNull();

        var dbArticle = await _context.Articles.FindAsync(request.ArticleId);
        dbArticle.Should().NotBeNull();
        dbArticle!.Tags.Should().Contain(t => t.Id == request.TagId);
    }
}
