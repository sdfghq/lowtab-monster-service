using FluentAssertions;
using Lowtab.Monster.Service.Application.Articles.Handlers;
using Lowtab.Monster.Service.Application.Articles.Queryes;
using Lowtab.Monster.Service.Domain.Entities;
using Lowtab.Monster.Service.Infrastructure.Persistence;
using Microsoft.Extensions.Logging.Abstractions;
using Xunit;

namespace Lowtab.Monster.Service.Application.UnitTests.Articles.Handlers;

#pragma warning disable VSTHRD002
public sealed class GetArticleHandlerTests : IDisposable, IAsyncDisposable
{
    private static readonly InternalDbContextFactory Factory = new();
    private readonly InternalDbContext _context = Factory.CreateDbContext([nameof(GetArticleHandlerTests)]);

    public GetArticleHandlerTests()
    {
        SeedData(_context, 5).GetAwaiter().GetResult();
        ExistingArticle = _context.Articles.First();
    }

    private ArticleEntity ExistingArticle { get; }

    public ValueTask DisposeAsync()
    {
        return _context.DisposeAsync();
    }

    public void Dispose()
    {
        _context.Dispose();
    }

    private static async Task SeedData(InternalDbContext context, int count)
    {
        var entities =
            Enumerable.Range(0, count).Select(_ => Arrange.GenerateArticleEntity());

        await context.Articles.AddRangeAsync(entities);
        await context.SaveChangesAsync(CancellationToken.None);
    }

    [Fact]
    public async Task GetArticleHandler_Handle_Successful()
    {
        // Arrange
        var handler = new GetArticleHandler(NullLogger<GetArticleHandler>.Instance, _context);
        var request = new GetArticleQuery { Id = ExistingArticle.Id };

        // Act
        var result = await handler.Handle(request, CancellationToken.None);

        // Assert
        result.Should().NotBeNull().And.BeEquivalentTo(ExistingArticle, options => options.ExcludingMissingMembers());
    }
}
