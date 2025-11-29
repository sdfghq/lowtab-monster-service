using FluentAssertions;
using Microsoft.Extensions.Logging.Abstractions;
using Lowtab.Monster.Service.Application.GroupTags.Handlers;
using Lowtab.Monster.Service.Infrastructure.Persistence;
using Xunit;

namespace Lowtab.Monster.Service.Application.UnitTests.GroupTags.Handlers;

public sealed class CreateGroupTagHandlerTests : IDisposable, IAsyncDisposable
{
    private static readonly InternalDbContextFactory Factory = new();
    private readonly InternalDbContext _context = Factory.CreateDbContext([nameof(CreateGroupTagHandlerTests)]);

    public ValueTask DisposeAsync() => _context.DisposeAsync();
    public void Dispose() => _context.Dispose();

    [Fact]
    public async Task CreateGroupTagHandler_Handle_Successful()
    {
        // Arrange
        var handler = new CreateGroupTagHandler(NullLogger<CreateGroupTagHandler>.Instance, _context);
        var request = Arrange.GetValidCreateGroupTagCommand();

        // Act
        var result = await handler.Handle(request, CancellationToken.None);

        // Assert
        result.Should().NotBeNull();
        result.Id.Should().NotBeEmpty();
        var dbEntity = await _context.GroupTags.FindAsync(result.Id);
        dbEntity.Should().NotBeNull().And.BeEquivalentTo(request, options => options.ExcludingMissingMembers());
    }
}
