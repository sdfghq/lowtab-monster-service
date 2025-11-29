using FluentAssertions;
using Lowtab.Monster.Service.Application.GroupTags.Commands;
using Lowtab.Monster.Service.Application.GroupTags.Handlers;
using Lowtab.Monster.Service.Domain.Entities;
using Lowtab.Monster.Service.Infrastructure.Persistence;
using Xunit;

namespace Lowtab.Monster.Service.Application.UnitTests.GroupTags.Handlers;

#pragma warning disable VSTHRD002
public sealed class DeleteGroupTagHandlerTests : IDisposable, IAsyncDisposable
{
    private static readonly InternalDbContextFactory Factory = new();
    private readonly InternalDbContext _context = Factory.CreateDbContext([nameof(DeleteGroupTagHandlerTests)]);

    public DeleteGroupTagHandlerTests()
    {
        SeedData(_context, 1).GetAwaiter().GetResult();
        ExistingGroupTag = _context.GroupTags.First();
    }

    private GroupTagEntity ExistingGroupTag { get; }

    public ValueTask DisposeAsync() => _context.DisposeAsync();
    public void Dispose() => _context.Dispose();

    private static async Task SeedData(InternalDbContext context, int count)
    {
        var entities =
            Enumerable.Range(0, count).Select(_ => Arrange.GenerateGroupTagEntity());

        await context.GroupTags.AddRangeAsync(entities);
        await context.SaveChangesAsync(CancellationToken.None);
    }

    [Fact]
    public async Task DeleteGroupTagHandler_Handle_Successful()
    {
        // Arrange
        var handler = new DeleteGroupTagHandler(_context);
        var request = new DeleteGroupTagCommand { Id = ExistingGroupTag.Id };

        // Act
        var result = await handler.Handle(request, CancellationToken.None);

        // Assert
        result.Should().NotBeNull();

        var dbEntity = await _context.GroupTags.FindAsync(request.Id);
        dbEntity.Should().BeNull();
    }
}
