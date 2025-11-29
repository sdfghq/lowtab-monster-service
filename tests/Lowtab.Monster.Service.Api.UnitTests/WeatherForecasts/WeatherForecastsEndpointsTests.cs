using System.Net;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using FluentAssertions;
using Lowtab.Monster.Service.Application.UnitTests.WeatherForecasts;
using Lowtab.Monster.Service.Application.WeatherForecasts.Commands;
using Lowtab.Monster.Service.Application.WeatherForecasts.Queryes;
using Lowtab.Monster.Service.Contracts.SerializationSettings;
using Lowtab.Monster.Service.Contracts.WeatherForecasts.CreateWeatherForecast;
using Lowtab.Monster.Service.Contracts.WeatherForecasts.GetWeatherForecast;
using Mediator;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.DependencyInjection;
using Moq;
using Xunit;

namespace Lowtab.Monster.Service.Api.UnitTests.WeatherForecasts;

public sealed class WeatherForecastsEndpointsTests : IDisposable, IAsyncDisposable
{
    private static readonly JsonSerializerOptions SerializerOptions =
        new JsonSerializerOptions().ConfigureJsonSerializerOptions();

    private readonly WebApplicationFactory<Program> _factory;
    private readonly Mock<IMediator> _mediatorMock = new();

    public WeatherForecastsEndpointsTests()
    {
        _factory = new CustomWebApplicationFactory().WithWebHostBuilder(builder =>
            builder.ConfigureServices(services =>
            {
                services.AddSingleton<IMediator>(_ => _mediatorMock.Object);
                services.AddSingleton<ISender>(sp => sp.GetRequiredService<IMediator>());
                services.AddSingleton<IPublisher>(sp => sp.GetRequiredService<IMediator>());
            }));
    }

    public ValueTask DisposeAsync()
    {
        return _factory.DisposeAsync();
    }

    public void Dispose()
    {
        _factory.Dispose();
    }

    [Fact]
    public async Task CreateWeatherForecast_Successful()
    {
        // Arrange
        var responseMock = new CreateWeatherForecastResponse { Id = Guid.NewGuid() };

        _mediatorMock.Setup(x =>
                x.Send(It.IsAny<CreateWeatherForecastCommand>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync((CreateWeatherForecastCommand _, CancellationToken _) => responseMock);

        using var client = _factory.CreateClient();
        var request = Arrange.GetValidCreateWeatherForecastCommand();

        using var requestMessage = new HttpRequestMessage(HttpMethod.Post, "/internal/v1/weatherforecast");
        requestMessage.Content = new StringContent(JsonSerializer.Serialize(request, SerializerOptions), Encoding.UTF8,
            new MediaTypeHeaderValue("application/json"));

        // Act
        using var response = await client.SendAsync(requestMessage);

        // Assert
        response.Should().NotBeNull();
        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        _mediatorMock.Verify(x => x.Send(It.IsAny<CreateWeatherForecastCommand>(), It.IsAny<CancellationToken>()),
            Times.Once);

        var responseContent = await response.Content.ReadAsStringAsync();
        var responseObject =
            JsonSerializer.Deserialize<CreateWeatherForecastResponse>(responseContent, SerializerOptions);
        responseObject.Should().NotBeNull().And.BeEquivalentTo(responseMock);
    }

    [Fact]
    public async Task GetWeatherForecast_Successful()
    {
        // Arrange
        var responseMock = new GetWeatherForecastResponse
        {
            Id = Guid.NewGuid(),
            CreatedAt = DateTimeOffset.UtcNow,
            UpdatedAt = DateTimeOffset.UtcNow,
            Date = DateOnly.FromDateTime(DateTime.UtcNow),
            Summary = "Summary",
            TemperatureC = 0
        };

        _mediatorMock.Setup(x =>
                x.Send(It.IsAny<GetWeatherForecastQuery>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync((GetWeatherForecastQuery _, CancellationToken _) => responseMock);

        using var client = _factory.CreateClient();
        using var request = new HttpRequestMessage(HttpMethod.Get, $"/internal/v1/weatherforecast/{Guid.NewGuid()}");

        // Act
        using var response = await client.SendAsync(request);

        // Assert
        response.Should().NotBeNull();
        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        _mediatorMock.Verify(x =>
            x.Send(It.IsAny<GetWeatherForecastQuery>(), It.IsAny<CancellationToken>()), Times.Once);

        var responseContent = await response.Content.ReadAsStringAsync();
        var responseObject = JsonSerializer.Deserialize<GetWeatherForecastResponse>(responseContent, SerializerOptions);
        responseObject.Should().NotBeNull().And.BeEquivalentTo(responseMock);
    }
}
