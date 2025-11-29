using FluentAssertions;
using Microsoft.Extensions.Logging.Abstractions;
using Lowtab.Monster.Service.Application.GroupTags.Handlers;
using Lowtab.Monster.Service.Application.GroupTags.Queryes;
using Lowtab.Monster.Service.Domain.Entities;
using Lowtab.Monster.Service.Infrastructure.Persistence;
using Xunit;

namespace Lowtab.Monster.Service.Application.UnitTests.GroupTags.Handlers;

#pragma warning disable VSTHRD002
public sealed class GetGroupTagHandlerTests : IDisposable, IAsyncDisposable
{
    private static readonly InternalDbContextFactory Factory = new();
    private readonly InternalDbContext _context = Factory.CreateDbContext([nameof(GetGroupTagHandlerTests)]);

    public GetGroupTagHandlerTests()
    {
        SeedData(_context, 5).GetAwaiter().GetResult();
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
    public async Task GetGroupTagHandler_Handle_Successful()
    {
        // Arrange
        var handler = new GetGroupTagHandler(NullLogger<GetGroupTagHandler>.Instance, _context);
        var request = new GetGroupTagQuery { Id = ExistingGroupTag.Id };

        // Act
        var result = await handler.Handle(request, CancellationToken.None);

        // Assert
        result.Should().NotBeNull().And.BeEquivalentTo(ExistingGroupTag, options => options.ExcludingMissingMembers());
    }
}
