using FluentAssertions;
using Lowtab.Monster.Service.Application.Articles.Handlers;
using Lowtab.Monster.Service.Application.Articles.Queryes;
using Lowtab.Monster.Service.Domain.Entities;
using Lowtab.Monster.Service.Infrastructure.Persistence;
using Xunit;

namespace Lowtab.Monster.Service.Application.UnitTests.Articles.Handlers;

#pragma warning disable VSTHRD002
public sealed class GetArticlesHandlerTests : IDisposable, IAsyncDisposable
{
    private static readonly InternalDbContextFactory Factory = new();
    private readonly InternalDbContext _context = Factory.CreateDbContext([nameof(GetArticlesHandlerTests)]);

    public GetArticlesHandlerTests()
    {
        SeedData(_context, 2).GetAwaiter().GetResult();
        ExistingArticle = _context.Articles.First();
    }

    private ArticleEntity ExistingArticle { get; }

    public ValueTask DisposeAsync() => _context.DisposeAsync();
    public void Dispose() => _context.Dispose();

    private static async Task SeedData(InternalDbContext context, int count)
    {
        var entities =
            Enumerable.Range(0, count).Select(_ => Arrange.GenerateArticleEntity());

        await context.Articles.AddRangeAsync(entities);
        await context.SaveChangesAsync(CancellationToken.None);
    }

    [Fact]
    public async Task GetArticlesHandler_Handle_Successful()
    {
        // Arrange
        var handler = new GetArticlesHandler(_context);
        var request = new GetArticlesQuery
        {
            Offset = 0,
            Limit = 10
        };

        // Act
        var result = await handler.Handle(request, CancellationToken.None);

        // Assert
        result.Should().NotBeNull();
        result.Items.Count.Should().Be(2);
        Assert.Equivalent(result.TotalCount, result.TotalFound);

        result = await handler.Handle(new GetArticlesQuery
        {
            Offset = 0,
            Limit = 1
        }, CancellationToken.None);
        result.Should().NotBeNull();
        result.TotalFound.Should().Be(result.TotalFound).And.Be(2);
        Assert.Equivalent(1, result.Items.Count);

        result = await handler.Handle(new GetArticlesQuery
        {
            Offset = 1,
            Limit = 1
        }, CancellationToken.None);
        result.Should().NotBeNull();
        result.TotalFound.Should().Be(result.TotalFound).And.Be(2);
        Assert.Equivalent(1, result.Items.Count);
    }
}
