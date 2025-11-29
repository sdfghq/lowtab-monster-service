using FluentAssertions;
using Lowtab.Monster.Service.Application.Tags.Handlers;
using Lowtab.Monster.Service.Application.Tags.Queryes;
using Lowtab.Monster.Service.Domain.Entities;
using Lowtab.Monster.Service.Infrastructure.Persistence;
using Xunit;

namespace Lowtab.Monster.Service.Application.UnitTests.Tags.Handlers;

#pragma warning disable VSTHRD002
public sealed class GetTagsHandlerTests : IDisposable, IAsyncDisposable
{
    private static readonly InternalDbContextFactory Factory = new();
    private readonly InternalDbContext _context = Factory.CreateDbContext([nameof(GetTagsHandlerTests)]);

    public GetTagsHandlerTests()
    {
        SeedData(_context, 2).GetAwaiter().GetResult();
        ExistingTag = _context.Tags.First();
    }

    private TagEntity ExistingTag { get; }

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
            Enumerable.Range(0, count).Select(_ => Arrange.GenerateTagEntity());

        await context.Tags.AddRangeAsync(entities);
        await context.SaveChangesAsync(CancellationToken.None);
    }

    [Fact]
    public async Task GetTagsHandler_Handle_Successful()
    {
        // Arrange
        var handler = new GetTagsHandler(_context);
        var request = new GetTagsQuery { Offset = 0, Limit = 10 };

        // Act
        var result = await handler.Handle(request, CancellationToken.None);

        // Assert
        result.Should().NotBeNull();
        result.Items.Count.Should().Be(2);
        Assert.Equivalent(result.TotalCount, result.TotalFound);

        result = await handler.Handle(new GetTagsQuery { Offset = 0, Limit = 1 }, CancellationToken.None);
        result.Should().NotBeNull();
        result.TotalFound.Should().Be(result.TotalFound).And.Be(2);
        Assert.Equivalent(1, result.Items.Count);

        result = await handler.Handle(new GetTagsQuery { Offset = 1, Limit = 1 }, CancellationToken.None);
        result.Should().NotBeNull();
        result.TotalFound.Should().Be(result.TotalFound).And.Be(2);
        Assert.Equivalent(1, result.Items.Count);
    }
}
