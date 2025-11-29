using System.Net;
using System.Text.Json;
using FluentAssertions;
using Lowtab.Monster.Service.Contracts.SerializationSettings;
using Microsoft.AspNetCore.Mvc;
using Xunit;

namespace Lowtab.Monster.Service.Api.UnitTests;

public sealed class WebApiObservabilityTests : IDisposable, IAsyncDisposable
{
    private static readonly JsonSerializerOptions SerializerOptions =
        new JsonSerializerOptions().ConfigureJsonSerializerOptions();

    private readonly CustomWebApplicationFactory _factory = new();

    public ValueTask DisposeAsync()
    {
        return _factory.DisposeAsync();
    }

    public void Dispose()
    {
        _factory.Dispose();
    }

    [Fact]
    public async Task WebApi_Swagger_Successfully()
    {
        using var client = _factory.CreateClient();
        using var response = await client.GetAsync("/swagger");
        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
    }

    [Fact]
    public async Task WebApi_Metrics_Successfully()
    {
        using var client = _factory.CreateClient();
        using var response = await client.GetAsync("/metrics");
        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
    }

    [Fact]
    public async Task WebApi_HealthChecks_Successfully()
    {
        using var client = _factory.CreateClient();

        using var readyResponse = await client.GetAsync("/ready");
        Assert.NotEqual(HttpStatusCode.NotFound, readyResponse.StatusCode);

        using var healthResponse = await client.GetAsync("/healthz");
        Assert.Equal(HttpStatusCode.OK, healthResponse.StatusCode);
    }

    [Fact]
    public async Task WebApi_NotFound_ResponseWithProblemDetails()
    {
        using var client = _factory.CreateClient();
        using var response = await client.GetAsync("/not-found");

        // Assert
        response.Should().NotBeNull();
        Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
        var content = await response.Content.ReadAsStringAsync();
        var problemDetails = JsonSerializer.Deserialize<ProblemDetails>(content, SerializerOptions)!;
        problemDetails.Should().NotBeNull();
        problemDetails.Status.Should().NotBeNull();
        problemDetails.Title.Should().NotBeNull();
        problemDetails.Type.Should().NotBeNull();
    }
}
