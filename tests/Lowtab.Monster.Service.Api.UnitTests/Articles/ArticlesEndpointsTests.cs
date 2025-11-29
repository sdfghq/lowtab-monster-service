using System.Net;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using DeepEqual.Syntax;
using FluentAssertions;
using Lowtab.Monster.Service.Application.Articles.Commands;
using Lowtab.Monster.Service.Application.Articles.Queryes;
using Lowtab.Monster.Service.Application.Common.Exceptions;
using Lowtab.Monster.Service.Application.UnitTests.Articles;
using Lowtab.Monster.Service.Contracts.Articles.CreateArticle;
using Lowtab.Monster.Service.Contracts.Articles.DeleteArticle;
using Lowtab.Monster.Service.Contracts.Articles.GetArticle;
using Lowtab.Monster.Service.Contracts.Articles.GetArticles;
using Lowtab.Monster.Service.Contracts.Articles.UpdateArticle;
using Lowtab.Monster.Service.Contracts.Articles.AddTagToArticle;
using Lowtab.Monster.Service.Contracts.GroupTags;
using Lowtab.Monster.Service.Contracts.SerializationSettings;
using Mediator;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.DependencyInjection;
using Moq;
using Xunit;

namespace Lowtab.Monster.Service.Api.UnitTests.Articles;

public sealed class ArticlesEndpointsTests : IDisposable, IAsyncDisposable
{
    private readonly WebApplicationFactory<Program> _factory;
    private readonly Mock<IMediator> _mediatorMock = new();

    private readonly JsonSerializerOptions _serializerOptions =
        new JsonSerializerOptions().ConfigureJsonSerializerOptions();

    public ArticlesEndpointsTests()
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
    public async Task CreateArticle_Successfully()
    {
        // Arrange
        var responseMock = new CreateArticleResponse { Id = Guid.NewGuid() };

        _mediatorMock.Setup(x => x.Send(It.IsAny<CreateArticleCommand>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync((CreateArticleCommand _, CancellationToken _) => responseMock);

        using var client = _factory.CreateClient();
        var request = Arrange.GetValidCreateArticleCommand();

        using var requestMessage = new HttpRequestMessage(HttpMethod.Post, "/internal/v1/Article")
        {
            Content = new StringContent(JsonSerializer.Serialize(request, _serializerOptions), Encoding.UTF8,
                new MediaTypeHeaderValue("application/json"))
        };

        // Act
        using var response = await client.SendAsync(requestMessage);

        // Assert
        response.Should().NotBeNull();
        response.StatusCode.Should().Be(HttpStatusCode.OK);

        _mediatorMock.Verify(x => x.Send(It.Is<CreateArticleCommand>(command => command.IsDeepEqual(request)),
            It.IsAny<CancellationToken>()), Times.Once);

        var responseContent = await response.Content.ReadAsStringAsync();
        var responseObject =
            JsonSerializer.Deserialize<CreateArticleResponse>(responseContent, _serializerOptions);
        responseObject.Should().NotBeNull().And.BeEquivalentTo(responseMock);
    }

    [Fact]
    public async Task CreateArticle_ValidationError_ResponseProblemDetails()
    {
        // Arrange
        using var client = _factory.CreateClient();

        using var request = new HttpRequestMessage(HttpMethod.Post, "/internal/v1/Article")
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
    public async Task GetArticle_Successfully()
    {
        // Arrange
        var request = new GetArticleQuery { Id = Guid.NewGuid() };
        var responseMock = Arrange.GetValidGetArticleResponse();

        _mediatorMock.Setup(x => x.Send(It.IsAny<GetArticleQuery>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync((GetArticleQuery _, CancellationToken _) => responseMock);

        using var client = _factory.CreateClient();
        using var requestMessage = new HttpRequestMessage(HttpMethod.Get, $"/internal/v1/Article/{request.Id}");

        // Act
        using var response = await client.SendAsync(requestMessage);

        // Assert
        response.Should().NotBeNull();
        response.StatusCode.Should().Be(HttpStatusCode.OK);

        _mediatorMock.Verify(x => x.Send(It.Is<GetArticleQuery>(query => query.IsDeepEqual(request)),
            It.IsAny<CancellationToken>()), Times.Once);

        var responseContent = await response.Content.ReadAsStringAsync();
        var responseObject = JsonSerializer.Deserialize<GetArticleResponse>(responseContent, _serializerOptions);
        responseObject.Should().NotBeNull().And.BeEquivalentTo(responseMock);
    }

    [Fact]
    public async Task GetArticle_NotFound_ResponseProblemDetails()
    {
        // Arrange
        var id = Guid.NewGuid();
        _mediatorMock.Setup(x => x.Send(It.IsAny<GetArticleQuery>(), It.IsAny<CancellationToken>()))
            .ThrowsAsync(new NotFoundException("Article not found"));

        using var client = _factory.CreateClient();

        using var request = new HttpRequestMessage(HttpMethod.Get, $"/internal/v1/Article/{id}");

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
    public async Task UpdateArticle_Successfully()
    {
        // Arrange
        var request = Arrange.GetValidUpdateArticleCommand();
        var responseMock = new UpdateArticleResponse();

        _mediatorMock.Setup(x => x.Send(It.IsAny<UpdateArticleCommand>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync((UpdateArticleCommand _, CancellationToken _) => responseMock);

        using var client = _factory.CreateClient();

        using var requestMessage = new HttpRequestMessage(HttpMethod.Put, $"/internal/v1/Article/{request.Id}")
        {
            Content = new StringContent(JsonSerializer.Serialize(request,
                _serializerOptions), Encoding.UTF8, new MediaTypeHeaderValue("application/json"))
        };

        // Act
        using var response = await client.SendAsync(requestMessage);

        // Assert
        response.Should().NotBeNull();
        response.StatusCode.Should().Be(HttpStatusCode.OK);

        _mediatorMock.Verify(x => x.Send(It.Is<UpdateArticleCommand>(command => command.IsDeepEqual(request)),
            It.IsAny<CancellationToken>()), Times.Once);

        var responseContent = await response.Content.ReadAsStringAsync();
        JsonSerializer.Deserialize<UpdateArticleResponse>(responseContent, _serializerOptions);
    }

    [Fact]
    public async Task UpdateArticle_ValidationError_ResponseProblemDetails()
    {
        // Arrange
        var request = Arrange.GetValidUpdateArticleCommand();
        using var client = _factory.CreateClient();

        using var requestMessage = new HttpRequestMessage(HttpMethod.Put, $"/internal/v1/Article/{request.Id}")
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
    public async Task DeleteArticle_Successfully()
    {
        // Arrange
        var request = new DeleteArticleCommand { Id = Guid.NewGuid() };
        var responseMock = new DeleteArticleResponse();

        _mediatorMock.Setup(x => x.Send(It.IsAny<DeleteArticleCommand>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync((DeleteArticleCommand _, CancellationToken _) => responseMock);

        using var client = _factory.CreateClient();
        using var requestMessage = new HttpRequestMessage(HttpMethod.Delete, $"/internal/v1/Article/{request.Id}");

        // Act
        using var response = await client.SendAsync(requestMessage);

        // Assert
        response.Should().NotBeNull();
        response.StatusCode.Should().Be(HttpStatusCode.OK);

        _mediatorMock.Verify(x => x.Send(It.Is<DeleteArticleCommand>(command => command.IsDeepEqual(request)),
            It.IsAny<CancellationToken>()), Times.Once);

        var responseContent = await response.Content.ReadAsStringAsync();
        JsonSerializer.Deserialize<DeleteArticleResponse>(responseContent, _serializerOptions);
    }

    [Fact]
    public async Task DeleteArticle_NotFound_ResponseProblemDetails()
    {
        // Arrange
        var id = Guid.NewGuid();
        _mediatorMock.Setup(x => x.Send(It.IsAny<DeleteArticleCommand>(), It.IsAny<CancellationToken>()))
            .ThrowsAsync(new NotFoundException("Article not found"));

        using var client = _factory.CreateClient();

        using var request = new HttpRequestMessage(HttpMethod.Delete, $"/internal/v1/Article/{id}");

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
    public async Task GetArticles_Successfully()
    {
        // Arrange
        var request = new GetArticlesQuery { Offset = 0, Limit = 10, TextFilter = "testname", TagFilter = null };

        var responseMock = new GetArticlesResponse
        {
            Items =
            [
                Arrange.GetValidGetArticleResponse(),
                Arrange.GetValidGetArticleResponse()
            ],
            TotalCount = 2,
            TotalFound = 2
        };

        _mediatorMock.Setup(x => x.Send(It.IsAny<GetArticlesQuery>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync((GetArticlesQuery _, CancellationToken _) => responseMock);

        using var client = _factory.CreateClient();
        using var requestMessage = new HttpRequestMessage(HttpMethod.Get,
            $"/internal/v1/Article/{request.Offset}/{request.Limit}?textFilter=testname");

        // Act
        using var response = await client.SendAsync(requestMessage);

        // Assert
        response.Should().NotBeNull();
        response.StatusCode.Should().Be(HttpStatusCode.OK);

        _mediatorMock.Verify(x => x.Send(It.Is<GetArticlesQuery>(query => query.IsDeepEqual(request)),
            It.IsAny<CancellationToken>()), Times.Once);

        var responseContent = await response.Content.ReadAsStringAsync();
        var responseObject =
            JsonSerializer.Deserialize<GetArticlesResponse>(responseContent, _serializerOptions);
        responseObject.Should().NotBeNull().And.BeEquivalentTo(responseMock);
    }

    [Fact]
    public async Task AddTagToArticle_Successfully()
    {
        // Arrange
        var articleId = Guid.NewGuid();
        var tagId = "test-tag";
        var group = GroupTagEnum.Map;
        var responseMock = new AddTagToArticleResponse();

        _mediatorMock.Setup(x => x.Send(It.IsAny<AddTagToArticleCommand>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync((AddTagToArticleCommand _, CancellationToken _) => responseMock);

        using var client = _factory.CreateClient();
        using var requestMessage = new HttpRequestMessage(HttpMethod.Put,
            $"/internal/v1/Article/{articleId}/tag/{tagId}/{group}");

        // Act
        using var response = await client.SendAsync(requestMessage);

        // Assert
        response.Should().NotBeNull();
        response.StatusCode.Should().Be(HttpStatusCode.OK);

        _mediatorMock.Verify(x => x.Send(It.Is<AddTagToArticleCommand>(command =>
                command.ArticleId == articleId &&
                command.TagId == tagId &&
                command.Group == group),
            It.IsAny<CancellationToken>()), Times.Once);
    }
}
