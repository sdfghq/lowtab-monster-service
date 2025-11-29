using FluentAssertions;
using Lowtab.Monster.Service.Application.GroupTags.Handlers;
using Lowtab.Monster.Service.Application.GroupTags.Queryes;
using Lowtab.Monster.Service.Domain.Entities;
using Lowtab.Monster.Service.Infrastructure.Persistence;
using Xunit;

namespace Lowtab.Monster.Service.Application.UnitTests.GroupTags.Handlers;

#pragma warning disable VSTHRD002
public sealed class GetGroupTagsHandlerTests : IDisposable, IAsyncDisposable
{
    private static readonly InternalDbContextFactory Factory = new();
    private readonly InternalDbContext _context = Factory.CreateDbContext([nameof(GetGroupTagsHandlerTests)]);

    public GetGroupTagsHandlerTests()
    {
        SeedData(_context, 2).GetAwaiter().GetResult();
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
    public async Task GetGroupTagsHandler_Handle_Successful()
    {
        // Arrange
        var handler = new GetGroupTagsHandler(_context);
        var request = new GetGroupTagsQuery
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

        result = await handler.Handle(new GetGroupTagsQuery
        {
            Offset = 0,
            Limit = 1
        }, CancellationToken.None);
        result.Should().NotBeNull();
        result.TotalFound.Should().Be(result.TotalFound).And.Be(2);
        Assert.Equivalent(1, result.Items.Count);

        result = await handler.Handle(new GetGroupTagsQuery
        {
            Offset = 1,
            Limit = 1
        }, CancellationToken.None);
        result.Should().NotBeNull();
        result.TotalFound.Should().Be(result.TotalFound).And.Be(2);
        Assert.Equivalent(1, result.Items.Count);
    }
}
