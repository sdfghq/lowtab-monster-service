using FluentAssertions;
using Microsoft.Extensions.Logging.Abstractions;
using Lowtab.Monster.Service.Application.Tags.Handlers;
using Lowtab.Monster.Service.Infrastructure.Persistence;
using Xunit;

namespace Lowtab.Monster.Service.Application.UnitTests.Tags.Handlers;

public sealed class CreateTagHandlerTests : IDisposable, IAsyncDisposable
{
    private static readonly InternalDbContextFactory Factory = new();
    private readonly InternalDbContext _context = Factory.CreateDbContext([nameof(CreateTagHandlerTests)]);

    public ValueTask DisposeAsync() => _context.DisposeAsync();
    public void Dispose() => _context.Dispose();

    [Fact]
    public async Task CreateTagHandler_Handle_Successful()
    {
        // Arrange
        var handler = new CreateTagHandler(NullLogger<CreateTagHandler>.Instance, _context);
        var request = Arrange.GetValidCreateTagCommand();

        // Act
        var result = await handler.Handle(request, CancellationToken.None);

        // Assert
        result.Should().NotBeNull();
        result.Id.Should().NotBeEmpty();
        var dbEntity = await _context.Tags.FindAsync(result.Id);
        dbEntity.Should().NotBeNull().And.BeEquivalentTo(request, options => options.ExcludingMissingMembers());
    }
}
