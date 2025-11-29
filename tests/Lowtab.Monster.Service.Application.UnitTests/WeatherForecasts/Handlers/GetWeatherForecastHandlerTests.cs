using FluentAssertions;
using Lowtab.Monster.Service.Application.WeatherForecasts.Handlers;
using Lowtab.Monster.Service.Application.WeatherForecasts.Options;
using Lowtab.Monster.Service.Application.WeatherForecasts.Queryes;
using Lowtab.Monster.Service.Domain.Entities;
using Lowtab.Monster.Service.Infrastructure.Persistence;
using Microsoft.Extensions.Caching.Hybrid;
using Microsoft.Extensions.Logging.Abstractions;
using Microsoft.Extensions.Options;
using Sdf.Platform.Redis.Testing;
using Xunit;

namespace Lowtab.Monster.Service.Application.UnitTests.WeatherForecasts.Handlers;

#pragma warning disable VSTHRD002 // Avoid using async void methods, prefer async Task for test methods
public sealed class GetWeatherForecastHandlerTests : IDisposable, IAsyncDisposable
{
    private static readonly InternalDbContextFactory Factory = new();
    private readonly HybridCache _cache = new FakeHybridCache();
    private readonly InternalDbContext _context = Factory.CreateDbContext([nameof(GetWeatherForecastHandlerTests)]);

    public GetWeatherForecastHandlerTests()
    {
        ExistingWeatherForecast = _context.WeatherForecasts.Add(new WeatherForecastEntity
        {
            Date = DateOnly.FromDateTime(DateTime.UtcNow), TemperatureC = 15, Summary = "123"
        }).Entity;
        _context.SaveChangesAsync(CancellationToken.None).GetAwaiter().GetResult();
    }

    private WeatherForecastEntity ExistingWeatherForecast { get; }

    public async ValueTask DisposeAsync()
    {
        await _context.DisposeAsync();
        await ((IAsyncDisposable)_cache).DisposeAsync();
    }

    public void Dispose()
    {
        _context.Dispose();
        ((IDisposable)_cache).Dispose();
    }

    [Fact]
    public async Task GetWeatherForecastCacheHandler_Handle_Successful()
    {
        // Arrange
        var options = Options.Create(new WeatherForecastCacheOptions());
        var handler =
            new GetWeatherForecastWithCacheHandler(NullLogger<GetWeatherForecastWithCacheHandler>.Instance, _context,
                _cache, options);
        var request = new GetWeatherForecastQuery { Id = ExistingWeatherForecast.Id };

        // Act
        var result = await handler.Handle(request, CancellationToken.None);

        // Assert
        result.Should().NotBeNull();
        result.Should().BeEquivalentTo(ExistingWeatherForecast);
    }
}
