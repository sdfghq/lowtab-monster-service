using FluentAssertions;
using Lowtab.Monster.Service.Application.Articles.Commands;
using Lowtab.Monster.Service.Application.Articles.Handlers;
using Lowtab.Monster.Service.Domain.Entities;
using Lowtab.Monster.Service.Infrastructure.Persistence;
using Xunit;

namespace Lowtab.Monster.Service.Application.UnitTests.Articles.Handlers;

public sealed class DeleteTagFromArticleHandlerTests : IDisposable, IAsyncDisposable
{
    private static readonly InternalDbContextFactory Factory = new();
    private readonly InternalDbContext _context = Factory.CreateDbContext([nameof(DeleteTagFromArticleHandlerTests)]);

    public DeleteTagFromArticleHandlerTests()
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

        article.Tags = new List<TagEntity> { tag };

        await context.Articles.AddAsync(article);
        await context.Tags.AddAsync(tag);
        await context.SaveChangesAsync(CancellationToken.None);
    }

    [Fact]
    public async Task DeleteTagFromArticleHandler_Handle_Successful()
    {
        // Arrange
        var handler = new DeleteTagFromArticleHandler(_context);
        var request = new DeleteTagFromArticleCommand { ArticleId = ExistingArticle.Id, TagId = ExistingTag.Id };

        // Act
        var result = await handler.Handle(request, CancellationToken.None);

        // Assert
        result.Should().NotBeNull();

        var dbArticle = await _context.Articles.FindAsync(request.ArticleId);
        dbArticle.Should().NotBeNull();
        dbArticle!.Tags.Should().NotContain(t => t.Id == request.TagId);
    }
}
