using FluentAssertions;
using Lowtab.Monster.Service.Application.Tags.Commands;
using Lowtab.Monster.Service.Application.Tags.Handlers;
using Lowtab.Monster.Service.Domain.Entities;
using Lowtab.Monster.Service.Infrastructure.Persistence;
using Xunit;

namespace Lowtab.Monster.Service.Application.UnitTests.Tags.Handlers;

#pragma warning disable VSTHRD002
public sealed class DeleteTagHandlerTests : IDisposable, IAsyncDisposable
{
    private static readonly InternalDbContextFactory Factory = new();
    private readonly InternalDbContext _context = Factory.CreateDbContext([nameof(DeleteTagHandlerTests)]);

    public DeleteTagHandlerTests()
    {
        SeedData(_context, 1).GetAwaiter().GetResult();
        ExistingTag = _context.Tags.First();
    }

    private TagEntity ExistingTag { get; }

    public ValueTask DisposeAsync() => _context.DisposeAsync();
    public void Dispose() => _context.Dispose();

    private static async Task SeedData(InternalDbContext context, int count)
    {
        var entities =
            Enumerable.Range(0, count).Select(_ => Arrange.GenerateTagEntity());

        await context.Tags.AddRangeAsync(entities);
        await context.SaveChangesAsync(CancellationToken.None);
    }

    [Fact]
    public async Task DeleteTagHandler_Handle_Successful()
    {
        // Arrange
        var handler = new DeleteTagHandler(_context);
        var request = new DeleteTagCommand { Id = ExistingTag.Id };

        // Act
        var result = await handler.Handle(request, CancellationToken.None);

        // Assert
        result.Should().NotBeNull();

        var dbEntity = await _context.Tags.FindAsync(request.Id);
        dbEntity.Should().BeNull();
    }
}
