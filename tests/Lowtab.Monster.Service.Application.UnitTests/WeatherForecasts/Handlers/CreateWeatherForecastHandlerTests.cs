using FluentAssertions;
using Lowtab.Monster.Service.Application.WeatherForecasts.Handlers;
using Lowtab.Monster.Service.Infrastructure.Persistence;
using Microsoft.Extensions.Logging.Abstractions;
using Xunit;

namespace Lowtab.Monster.Service.Application.UnitTests.WeatherForecasts.Handlers;

#pragma warning disable VSTHRD002 // Avoid using async void methods, prefer async Task for test methods
public sealed class CreateWeatherForecastHandlerTests : IDisposable, IAsyncDisposable
{
    private static readonly InternalDbContextFactory Factory = new();

    private readonly InternalDbContext _context = Factory.CreateDbContext([nameof(CreateWeatherForecastHandlerTests)]);

    public ValueTask DisposeAsync()
    {
        return _context.DisposeAsync();
    }

    public void Dispose()
    {
        _context.Dispose();
    }

    [Fact]
    public async Task CreateWeatherForecastHandler_Handle_Successful()
    {
        // Arrange
        var handler = new CreateWeatherForecastHandler(NullLogger<CreateWeatherForecastHandler>.Instance, _context);
        var request = Arrange.GetValidCreateWeatherForecastCommand();

        // Act
        var result = await handler.Handle(request, CancellationToken.None);

        // Assert
        result.Should().NotBeNull();
        result.Id.Should().NotBeEmpty();
        var dbEntity = await _context.WeatherForecasts.FindAsync(result.Id);
        dbEntity.Should().NotBeNull().And.BeEquivalentTo(request);
    }
}
