using FluentAssertions;
using Microsoft.Extensions.Logging.Abstractions;
using Lowtab.Monster.Service.Application.Articles.Handlers;
using Lowtab.Monster.Service.Infrastructure.Persistence;
using Xunit;

namespace Lowtab.Monster.Service.Application.UnitTests.Articles.Handlers;

public sealed class CreateArticleHandlerTests : IDisposable, IAsyncDisposable
{
    private static readonly InternalDbContextFactory Factory = new();
    private readonly InternalDbContext _context = Factory.CreateDbContext([nameof(CreateArticleHandlerTests)]);

    public ValueTask DisposeAsync() => _context.DisposeAsync();
    public void Dispose() => _context.Dispose();

    [Fact]
    public async Task CreateArticleHandler_Handle_Successful()
    {
        // Arrange
        var handler = new CreateArticleHandler(NullLogger<CreateArticleHandler>.Instance, _context);
        var request = Arrange.GetValidCreateArticleCommand();

        // Act
        var result = await handler.Handle(request, CancellationToken.None);

        // Assert
        result.Should().NotBeNull();
        result.Id.Should().NotBeEmpty();
        var dbEntity = await _context.Articles.FindAsync(result.Id);
        dbEntity.Should().NotBeNull().And.BeEquivalentTo(request, options => options.ExcludingMissingMembers());
    }
}
