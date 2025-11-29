using System.Net;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using DeepEqual.Syntax;
using FluentAssertions;
using Mediator;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.DependencyInjection;
using Moq;
using Lowtab.Monster.Service.Application.Common.Exceptions;
using Lowtab.Monster.Service.Application.UnitTests.GroupTags;
using Lowtab.Monster.Service.Application.GroupTags.Commands;
using Lowtab.Monster.Service.Application.GroupTags.Queryes;
using Lowtab.Monster.Service.Contracts.SerializationSettings;
using Lowtab.Monster.Service.Contracts.GroupTags.CreateGroupTag;
using Lowtab.Monster.Service.Contracts.GroupTags.DeleteGroupTag;
using Lowtab.Monster.Service.Contracts.GroupTags.GetGroupTag;
using Lowtab.Monster.Service.Contracts.GroupTags.GetGroupTags;
using Lowtab.Monster.Service.Contracts.GroupTags.UpdateGroupTag;
using Xunit;

namespace Lowtab.Monster.Service.Api.UnitTests.GroupTags;

public sealed class GroupTagsEndpointsTests : IDisposable, IAsyncDisposable
{
    private readonly WebApplicationFactory<Program> _factory;
    private readonly Mock<IMediator> _mediatorMock = new();

    private readonly JsonSerializerOptions _serializerOptions = new JsonSerializerOptions().ConfigureJsonSerializerOptions();

    public GroupTagsEndpointsTests()
    {
        _factory = new CustomWebApplicationFactory().WithWebHostBuilder(builder =>
            builder.ConfigureServices(services =>
            {
                services.AddSingleton<IMediator>(_ => _mediatorMock.Object);
                services.AddSingleton<ISender>(sp => sp.GetRequiredService<IMediator>());
                services.AddSingleton<IPublisher>(sp => sp.GetRequiredService<IMediator>());
            }));
    }

    public ValueTask DisposeAsync() => _factory.DisposeAsync();
    public void Dispose() => _factory.Dispose();

    [Fact]
    public async Task CreateGroupTag_Successfully()
    {
        // Arrange
        var responseMock = new CreateGroupTagResponse { Id = Guid.NewGuid() };

        _mediatorMock.Setup(x => x.Send(It.IsAny<CreateGroupTagCommand>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync((CreateGroupTagCommand _, CancellationToken _) => responseMock);

        using var client = _factory.CreateClient();
        var request = Arrange.GetValidCreateGroupTagCommand();

        using var requestMessage = new HttpRequestMessage(HttpMethod.Post, "/internal/v1/GroupTag")
        {
            Content = new StringContent(JsonSerializer.Serialize(request, _serializerOptions), Encoding.UTF8,
                new MediaTypeHeaderValue("application/json"))
        };

        // Act
        using var response = await client.SendAsync(requestMessage);

        // Assert
        response.Should().NotBeNull();
        response.StatusCode.Should().Be(HttpStatusCode.OK);

        _mediatorMock.Verify(x => x.Send(It.Is<CreateGroupTagCommand>(command => command.IsDeepEqual(request)),
            It.IsAny<CancellationToken>()), Times.Once);

        var responseContent = await response.Content.ReadAsStringAsync();
        var responseObject =
            JsonSerializer.Deserialize<CreateGroupTagResponse>(responseContent, _serializerOptions);
        responseObject.Should().NotBeNull().And.BeEquivalentTo(responseMock);
    }

    [Fact]
    public async Task CreateGroupTag_ValidationError_ResponseProblemDetails()
    {
        // Arrange
        using var client = _factory.CreateClient();

        using var request = new HttpRequestMessage(HttpMethod.Post, "/internal/v1/GroupTag")
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
    public async Task GetGroupTag_Successfully()
    {
        // Arrange
        var request = new GetGroupTagQuery { Id = Guid.NewGuid() };
        var responseMock = Arrange.GetValidGetGroupTagResponse();

        _mediatorMock.Setup(x => x.Send(It.IsAny<GetGroupTagQuery>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync((GetGroupTagQuery _, CancellationToken _) => responseMock);

        using var client = _factory.CreateClient();
        using var requestMessage = new HttpRequestMessage(HttpMethod.Get, $"/internal/v1/GroupTag/{request.Id}");

        // Act
        using var response = await client.SendAsync(requestMessage);

        // Assert
        response.Should().NotBeNull();
        response.StatusCode.Should().Be(HttpStatusCode.OK);

        _mediatorMock.Verify(x => x.Send(It.Is<GetGroupTagQuery>(query => query.IsDeepEqual(request)),
            It.IsAny<CancellationToken>()), Times.Once);

        var responseContent = await response.Content.ReadAsStringAsync();
        var responseObject = JsonSerializer.Deserialize<GetGroupTagResponse>(responseContent, _serializerOptions);
        responseObject.Should().NotBeNull().And.BeEquivalentTo(responseMock);
    }

    [Fact]
    public async Task GetGroupTag_NotFound_ResponseProblemDetails()
    {
        // Arrange
        var id = Guid.NewGuid();
        _mediatorMock.Setup(x => x.Send(It.IsAny<GetGroupTagQuery>(), It.IsAny<CancellationToken>()))
            .ThrowsAsync(new NotFoundException("GroupTag not found"));

        using var client = _factory.CreateClient();

        using var request = new HttpRequestMessage(HttpMethod.Get, $"/internal/v1/GroupTag/{id}");

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
    public async Task UpdateGroupTag_Successfully()
    {
        // Arrange
        var request = Arrange.GetValidUpdateGroupTagCommand();
        var responseMock = new UpdateGroupTagResponse();

        _mediatorMock.Setup(x => x.Send(It.IsAny<UpdateGroupTagCommand>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync((UpdateGroupTagCommand _, CancellationToken _) => responseMock);

        using var client = _factory.CreateClient();

        using var requestMessage = new HttpRequestMessage(HttpMethod.Put, $"/internal/v1/GroupTag/{request.Id}")
        {
            Content = new StringContent(JsonSerializer.Serialize(request,
                _serializerOptions), Encoding.UTF8, new MediaTypeHeaderValue("application/json"))
        };

        // Act
        using var response = await client.SendAsync(requestMessage);

        // Assert
        response.Should().NotBeNull();
        response.StatusCode.Should().Be(HttpStatusCode.OK);

        _mediatorMock.Verify(x => x.Send(It.Is<UpdateGroupTagCommand>(command => command.IsDeepEqual(request)),
            It.IsAny<CancellationToken>()), Times.Once);

        var responseContent = await response.Content.ReadAsStringAsync();
        JsonSerializer.Deserialize<UpdateGroupTagResponse>(responseContent, _serializerOptions);
    }

    [Fact]
    public async Task UpdateGroupTag_ValidationError_ResponseProblemDetails()
    {
        // Arrange
        var request = Arrange.GetValidUpdateGroupTagCommand();
        using var client = _factory.CreateClient();

        using var requestMessage = new HttpRequestMessage(HttpMethod.Put, $"/internal/v1/GroupTag/{request.Id}")
        {
            Content = new StringContent(string.Empty, Encoding.UTF8, new MediaTypeHeaderValue("application/json"))
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
    public async Task DeleteGroupTag_Successfully()
    {
        // Arrange
        var request = new DeleteGroupTagCommand { Id = Guid.NewGuid() };
        var responseMock = new DeleteGroupTagResponse();

        _mediatorMock.Setup(x => x.Send(It.IsAny<DeleteGroupTagCommand>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync((DeleteGroupTagCommand _, CancellationToken _) => responseMock);

        using var client = _factory.CreateClient();
        using var requestMessage = new HttpRequestMessage(HttpMethod.Delete, $"/internal/v1/GroupTag/{request.Id}");

        // Act
        using var response = await client.SendAsync(requestMessage);

        // Assert
        response.Should().NotBeNull();
        response.StatusCode.Should().Be(HttpStatusCode.OK);

        _mediatorMock.Verify(x => x.Send(It.Is<DeleteGroupTagCommand>(command => command.IsDeepEqual(request)),
            It.IsAny<CancellationToken>()), Times.Once);

        var responseContent = await response.Content.ReadAsStringAsync();
        JsonSerializer.Deserialize<DeleteGroupTagResponse>(responseContent, _serializerOptions);
    }

    [Fact]
    public async Task DeleteGroupTag_NotFound_ResponseProblemDetails()
    {
        // Arrange
        var id = Guid.NewGuid();
        _mediatorMock.Setup(x => x.Send(It.IsAny<DeleteGroupTagCommand>(), It.IsAny<CancellationToken>()))
            .ThrowsAsync(new NotFoundException("GroupTag not found"));

        using var client = _factory.CreateClient();

        using var request = new HttpRequestMessage(HttpMethod.Delete, $"/internal/v1/GroupTag/{id}");

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
    public async Task GetGroupTags_Successfully()
    {
        // Arrange
        var request = new GetGroupTagsQuery
        {
            Offset = 0,
            Limit = 10,
            NameFilter = "testname"
        };

        var responseMock = new GetGroupTagsResponse
        {
            Items =
            [
                Arrange.GetValidGetGroupTagResponse(),
                Arrange.GetValidGetGroupTagResponse()
            ],
            TotalCount = 2,
            TotalFound = 2
        };

        _mediatorMock.Setup(x => x.Send(It.IsAny<GetGroupTagsQuery>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync((GetGroupTagsQuery _, CancellationToken _) => responseMock);

        using var client = _factory.CreateClient();
        using var requestMessage = new HttpRequestMessage(HttpMethod.Get,
            $"/internal/v1/GroupTag/{request.Offset}/{request.Limit}?nameFilter=testname");

        // Act
        using var response = await client.SendAsync(requestMessage);

        // Assert
        response.Should().NotBeNull();
        response.StatusCode.Should().Be(HttpStatusCode.OK);

        _mediatorMock.Verify(x => x.Send(It.Is<GetGroupTagsQuery>(query => query.IsDeepEqual(request)),
            It.IsAny<CancellationToken>()), Times.Once);

        var responseContent = await response.Content.ReadAsStringAsync();
        var responseObject =
            JsonSerializer.Deserialize<GetGroupTagsResponse>(responseContent, _serializerOptions);
        responseObject.Should().NotBeNull().And.BeEquivalentTo(responseMock);
    }
}
