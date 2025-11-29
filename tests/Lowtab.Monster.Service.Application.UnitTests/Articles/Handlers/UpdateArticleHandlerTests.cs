using FluentAssertions;
using Microsoft.Extensions.Logging.Abstractions;
using Lowtab.Monster.Service.Application.Articles.Commands;
using Lowtab.Monster.Service.Application.Articles.Handlers;
using Lowtab.Monster.Service.Domain.Entities;
using Lowtab.Monster.Service.Infrastructure.Persistence;
using Xunit;

namespace Lowtab.Monster.Service.Application.UnitTests.Articles.Handlers;

#pragma warning disable VSTHRD002
public sealed class UpdateArticleHandlerTests : IDisposable, IAsyncDisposable
{
    private static readonly InternalDbContextFactory Factory = new();
    private readonly InternalDbContext _context = Factory.CreateDbContext([nameof(UpdateArticleHandlerTests)]);

    public UpdateArticleHandlerTests()
    {
        SeedData(_context, 5).GetAwaiter().GetResult();
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
    public async Task UpdateArticleHandler_Handle_Successful()
    {
        // Arrange
        var handler = new UpdateArticleHandler(NullLogger<UpdateArticleHandler>.Instance, _context);
        var request = new UpdateArticleCommand
        {
            Id = ExistingArticle.Id,
            Name = "test name"
        };

        // Act
        var result = await handler.Handle(request, CancellationToken.None);

        // Assert
        result.Should().NotBeNull();

        var dbEntity = await _context.Articles.FindAsync(request.Id);
        dbEntity.Should().NotBeNull().And.BeEquivalentTo(request, options => options.ExcludingMissingMembers());
    }
}
