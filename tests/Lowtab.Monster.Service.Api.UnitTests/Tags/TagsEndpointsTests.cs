using System.Net;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using DeepEqual.Syntax;
using FluentAssertions;
using Lowtab.Monster.Service.Application.Common.Exceptions;
using Lowtab.Monster.Service.Application.Tags.Commands;
using Lowtab.Monster.Service.Application.Tags.Queryes;
using Lowtab.Monster.Service.Application.UnitTests.Tags;
using Lowtab.Monster.Service.Contracts.SerializationSettings;
using Lowtab.Monster.Service.Contracts.Tags.Common;
using Lowtab.Monster.Service.Contracts.Tags.CreateTag;
using Lowtab.Monster.Service.Contracts.Tags.DeleteTag;
using Lowtab.Monster.Service.Contracts.Tags.GetTag;
using Lowtab.Monster.Service.Contracts.Tags.GetTags;
using Lowtab.Monster.Service.Contracts.Tags.UpdateTag;
using Mediator;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.DependencyInjection;
using Moq;
using Xunit;

namespace Lowtab.Monster.Service.Api.UnitTests.Tags;

public sealed class TagsEndpointsTests : IDisposable, IAsyncDisposable
{
    private readonly WebApplicationFactory<Program> _factory;
    private readonly Mock<IMediator> _mediatorMock = new();

    private readonly JsonSerializerOptions _serializerOptions =
        new JsonSerializerOptions().ConfigureJsonSerializerOptions();

    public TagsEndpointsTests()
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
    public async Task CreateTag_Successfully()
    {
        // Arrange
        var responseMock = new CreateTagResponse { Id = new TagId(GroupTagEnum.Map, Guid.NewGuid().ToString()) };

        _mediatorMock.Setup(x => x.Send(It.IsAny<CreateTagCommand>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync((CreateTagCommand _, CancellationToken _) => responseMock);

        using var client = _factory.CreateClient();
        var request = Arrange.GetValidCreateTagCommand();

        using var requestMessage = new HttpRequestMessage(HttpMethod.Post, "/internal/v1/Tag")
        {
            Content = new StringContent(JsonSerializer.Serialize(request, _serializerOptions), Encoding.UTF8,
                new MediaTypeHeaderValue("application/json"))
        };

        // Act
        using var response = await client.SendAsync(requestMessage);

        // Assert
        response.Should().NotBeNull();
        response.StatusCode.Should().Be(HttpStatusCode.OK);

        _mediatorMock.Verify(x => x.Send(It.Is<CreateTagCommand>(command => command.IsDeepEqual(request)),
            It.IsAny<CancellationToken>()), Times.Once);

        var responseContent = await response.Content.ReadAsStringAsync();
        var responseObject =
            JsonSerializer.Deserialize<CreateTagResponse>(responseContent, _serializerOptions);
        responseObject.Should().NotBeNull().And.BeEquivalentTo(responseMock);
    }

    [Fact]
    public async Task CreateTag_ValidationError_ResponseProblemDetails()
    {
        // Arrange
        using var client = _factory.CreateClient();

        using var request = new HttpRequestMessage(HttpMethod.Post, "/internal/v1/Tag")
        {
            Content = new StringContent(string.Empty, Encoding.UTF8, new MediaTypeHeaderValue("application/json"))
        };

        // Act
        using var response = await client.SendAsync(request);

        // Assert
        response.Should().NotBeNull();
        response.StatusCode.Should().Be(HttpStatusCode.BadRequest);

        var responseContent = await response.Content.ReadAsStringAsync();
        var problemDetails = JsonSerializer.Deserialize<ProblemDetails>(responseContent, _serializerOptions)!;
        problemDetails.Should().NotBeNull();
    }

    [Fact]
    public async Task GetTag_Successfully()
    {
        // Arrange
        var request = new GetTagQuery { Id = new TagId(GroupTagEnum.Map, Guid.NewGuid().ToString()) };
        var responseMock = Arrange.GetValidGetTagResponse();

        _mediatorMock.Setup(x => x.Send(It.IsAny<GetTagQuery>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync((GetTagQuery _, CancellationToken _) => responseMock);

        using var client = _factory.CreateClient();
        using var requestMessage =
            new HttpRequestMessage(HttpMethod.Get, $"/internal/v1/Tag/{request.Id.Id}/{request.Id.Group}");

        // Act
        using var response = await client.SendAsync(requestMessage);

        // Assert
        response.Should().NotBeNull();
        response.StatusCode.Should().Be(HttpStatusCode.OK);

        _mediatorMock.Verify(x => x.Send(It.Is<GetTagQuery>(query => query.IsDeepEqual(request)),
            It.IsAny<CancellationToken>()), Times.Once);

        var responseContent = await response.Content.ReadAsStringAsync();
        var responseObject = JsonSerializer.Deserialize<GetTagResponse>(responseContent, _serializerOptions);
        responseObject.Should().NotBeNull().And.BeEquivalentTo(responseMock);
    }

    [Fact]
    public async Task GetTag_NotFound_ResponseProblemDetails()
    {
        // Arrange
        var id = Guid.NewGuid().ToString();
        _mediatorMock.Setup(x => x.Send(It.IsAny<GetTagQuery>(), It.IsAny<CancellationToken>()))
            .ThrowsAsync(new NotFoundException("Tag not found"));

        using var client = _factory.CreateClient();

        using var request = new HttpRequestMessage(HttpMethod.Get, $"/internal/v1/Tag/{id}/{GroupTagEnum.Map}");

        // Act
        using var response = await client.SendAsync(request);

        // Assert
        response.Should().NotBeNull();
        response.StatusCode.Should().Be(HttpStatusCode.NotFound);

        var responseContent = await response.Content.ReadAsStringAsync();
        var problemDetails = JsonSerializer.Deserialize<ProblemDetails>(responseContent, _serializerOptions)!;
        problemDetails.Should().NotBeNull();
    }

    [Fact]
    public async Task UpdateTag_Successfully()
    {
        // Arrange
        var request = Arrange.GetValidUpdateTagCommand();
        var responseMock = new UpdateTagResponse();

        _mediatorMock.Setup(x => x.Send(It.IsAny<UpdateTagCommand>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync((UpdateTagCommand _, CancellationToken _) => responseMock);

        using var client = _factory.CreateClient();

        using var requestMessage =
            new HttpRequestMessage(HttpMethod.Put, $"/internal/v1/Tag/{request.Id.Id}/{request.Id.Group}")
            {
                Content = new StringContent(JsonSerializer.Serialize(request,
                    _serializerOptions), Encoding.UTF8, new MediaTypeHeaderValue("application/json"))
            };

        // Act
        using var response = await client.SendAsync(requestMessage);

        // Assert
        response.Should().NotBeNull();
        response.StatusCode.Should().Be(HttpStatusCode.OK);

        _mediatorMock.Verify(x => x.Send(It.Is<UpdateTagCommand>(command => command.IsDeepEqual(request)),
            It.IsAny<CancellationToken>()), Times.Once);

        var responseContent = await response.Content.ReadAsStringAsync();
        JsonSerializer.Deserialize<UpdateTagResponse>(responseContent, _serializerOptions);
    }

    [Fact]
    public async Task UpdateTag_ValidationError_ResponseProblemDetails()
    {
        // Arrange
        var request = Arrange.GetValidUpdateTagCommand();
        using var client = _factory.CreateClient();

        using var requestMessage =
            new HttpRequestMessage(HttpMethod.Put, $"/internal/v1/Tag/{request.Id.Id}/{request.Id.Group}")
            {
                Content = new StringContent(string.Empty, Encoding.UTF8,
                    new MediaTypeHeaderValue("application/json"))
            };

        // Act
        using var response = await client.SendAsync(requestMessage);

        // Assert
        response.Should().NotBeNull();
        response.StatusCode.Should().Be(HttpStatusCode.BadRequest);

        var responseContent = await response.Content.ReadAsStringAsync();
        var problemDetails = JsonSerializer.Deserialize<ProblemDetails>(responseContent, _serializerOptions)!;
        problemDetails.Should().NotBeNull();
    }

    [Fact]
    public async Task DeleteTag_Successfully()
    {
        // Arrange
        var request = new DeleteTagCommand { Id = new TagId(GroupTagEnum.Map, Guid.NewGuid().ToString()) };
        var responseMock = new DeleteTagResponse();

        _mediatorMock.Setup(x => x.Send(It.IsAny<DeleteTagCommand>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync((DeleteTagCommand _, CancellationToken _) => responseMock);

        using var client = _factory.CreateClient();
        using var requestMessage =
            new HttpRequestMessage(HttpMethod.Delete, $"/internal/v1/Tag/{request.Id.Id}/{request.Id.Group}");

        // Act
        using var response = await client.SendAsync(requestMessage);

        // Assert
        response.Should().NotBeNull();
        response.StatusCode.Should().Be(HttpStatusCode.OK);

        _mediatorMock.Verify(x => x.Send(It.Is<DeleteTagCommand>(command => command.IsDeepEqual(request)),
            It.IsAny<CancellationToken>()), Times.Once);

        var responseContent = await response.Content.ReadAsStringAsync();
        JsonSerializer.Deserialize<DeleteTagResponse>(responseContent, _serializerOptions);
    }

    [Fact]
    public async Task DeleteTag_NotFound_ResponseProblemDetails()
    {
        // Arrange
        var id = Guid.NewGuid().ToString();
        _mediatorMock.Setup(x => x.Send(It.IsAny<DeleteTagCommand>(), It.IsAny<CancellationToken>()))
            .ThrowsAsync(new NotFoundException("Tag not found"));

        using var client = _factory.CreateClient();

        using var request = new HttpRequestMessage(HttpMethod.Delete, $"/internal/v1/Tag/{id}/{GroupTagEnum.Map}");

        // Act
        using var response = await client.SendAsync(request);

        // Assert
        response.Should().NotBeNull();
        response.StatusCode.Should().Be(HttpStatusCode.NotFound);

        var responseContent = await response.Content.ReadAsStringAsync();
        var problemDetails = JsonSerializer.Deserialize<ProblemDetails>(responseContent, _serializerOptions)!;
        problemDetails.Should().NotBeNull();
    }

    [Fact]
    public async Task GetTags_Successfully()
    {
        // Arrange
        var request = new GetTagsQuery { Offset = 0, Limit = 10, IdFilter = "testname", GroupsFilter = [] };

        var responseMock = new GetTagsResponse
        {
            Items =
            [
                Arrange.GetValidGetTagResponse(),
                Arrange.GetValidGetTagResponse()
            ],
            TotalCount = 2,
            TotalFound = 2
        };

        _mediatorMock.Setup(x => x.Send(It.IsAny<GetTagsQuery>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync((GetTagsQuery _, CancellationToken _) => responseMock);

        using var client = _factory.CreateClient();
        using var requestMessage = new HttpRequestMessage(HttpMethod.Get,
            $"/internal/v1/Tags/{request.Offset}/{request.Limit}?idFilter=testname");

        // Act
        using var response = await client.SendAsync(requestMessage);

        // Assert
        response.Should().NotBeNull();
        response.StatusCode.Should().Be(HttpStatusCode.OK);

        _mediatorMock.Verify(x => x.Send(It.Is<GetTagsQuery>(query => query.IsDeepEqual(request)),
            It.IsAny<CancellationToken>()), Times.Once);

        var responseContent = await response.Content.ReadAsStringAsync();
        var responseObject =
            JsonSerializer.Deserialize<GetTagsResponse>(responseContent, _serializerOptions);
        responseObject.Should().NotBeNull().And.BeEquivalentTo(responseMock);
    }
}
