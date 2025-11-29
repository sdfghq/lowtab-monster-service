using System.Net;
using Lowtab.Monster.Service.Application.Articles.Commands;
using Lowtab.Monster.Service.Application.Articles.Queryes;
using Lowtab.Monster.Service.Contracts.Articles;
using Lowtab.Monster.Service.Contracts.Articles.AddTagToArticle;
using Lowtab.Monster.Service.Contracts.Articles.CreateArticle;
using Lowtab.Monster.Service.Contracts.Articles.DeleteArticle;
using Lowtab.Monster.Service.Contracts.Articles.GetArticle;
using Lowtab.Monster.Service.Contracts.Articles.GetArticles;
using Lowtab.Monster.Service.Contracts.Articles.UpdateArticle;
using Lowtab.Monster.Service.Contracts.GroupTags;
using Mediator;
using Microsoft.AspNetCore.Mvc;

namespace Lowtab.Monster.Service.Api.Endpoints;

internal static class ArticlesEndpoints
{
    private const string Tag = "Articles internal methods";

    public static WebApplication MapArticleEndpoints(this WebApplication app)
    {
        var api = app
            .NewVersionedApi(Tag)
            .HasApiVersion(1.0);

        api.MapPost(ArticlesRoutes.CreateArticle, CreateArticle);
        api.MapGet(ArticlesRoutes.GetArticle, GetArticle);
        api.MapGet(ArticlesRoutes.GetArticles, GetArticles);
        api.MapPut(ArticlesRoutes.UpdateArticle, UpdateArticle);
        api.MapPut(ArticlesRoutes.AddTagToArticle, AddTagToArticle);
        api.MapDelete(ArticlesRoutes.DeleteArticle, DeleteArticle);

        return app;
    }

    [ProducesResponseType(typeof(ProblemDetails), (int)HttpStatusCode.BadRequest)]
    [ProducesResponseType(typeof(ProblemDetails), (int)HttpStatusCode.InternalServerError)]
    [ProducesResponseType(typeof(ProblemDetails), (int)HttpStatusCode.NotFound)]
    [ProducesResponseType(typeof(AddTagToArticleResponse), (int)HttpStatusCode.OK)]
    private static async Task<AddTagToArticleResponse> AddTagToArticle(
        [FromRoute] Guid id,
        [FromRoute] string tagId,
        [FromRoute] GroupTagEnum group,
        [FromServices] ISender mediator,
        CancellationToken ct = default)
    {
        var result = await mediator.Send(new AddTagToArticleCommand
        {
            ArticleId = id,
            TagId = tagId,
            Group = group
        }, ct);
        return result;
    }

    [ProducesResponseType(typeof(ProblemDetails), (int)HttpStatusCode.BadRequest)]
    [ProducesResponseType(typeof(ProblemDetails), (int)HttpStatusCode.InternalServerError)]
    [ProducesResponseType(typeof(CreateArticleResponse), (int)HttpStatusCode.OK)]
    private static async Task<CreateArticleResponse> CreateArticle([FromBody] CreateArticleRequest request,
        [FromServices] ISender mediator, CancellationToken ct = default)
    {
        var result =
            await mediator.Send(
                new CreateArticleCommand
                {
                    Title = request.Title, Body = request.Body, PreviewImageUrl = request.PreviewImageUrl
                }, ct);
        return result;
    }

    [ProducesResponseType(typeof(ProblemDetails), (int)HttpStatusCode.BadRequest)]
    [ProducesResponseType(typeof(ProblemDetails), (int)HttpStatusCode.InternalServerError)]
    [ProducesResponseType(typeof(UpdateArticleResponse), (int)HttpStatusCode.OK)]
    private static async Task<UpdateArticleResponse> UpdateArticle(
        [FromRoute] Guid id, [FromBody] UpdateArticleRequest request, [FromServices] ISender mediator,
        CancellationToken ct = default)
    {
        var result =
            await mediator.Send(
                new UpdateArticleCommand
                {
                    Title = request.Title, Id = id, Body = request.Body, PreviewImageUrl = request.PreviewImageUrl
                }, ct);
        return result;
    }

    [ProducesResponseType(typeof(ProblemDetails), (int)HttpStatusCode.BadRequest)]
    [ProducesResponseType(typeof(ProblemDetails), (int)HttpStatusCode.NotFound)]
    [ProducesResponseType(typeof(ProblemDetails), (int)HttpStatusCode.InternalServerError)]
    [ProducesResponseType(typeof(CreateArticleResponse), (int)HttpStatusCode.OK)]
    private static async Task<GetArticleResponse> GetArticle([FromRoute] Guid id, [FromServices] ISender mediator,
        CancellationToken ct = default)
    {
        var result = await mediator.Send(new GetArticleQuery { Id = id }, ct);
        return result;
    }

    [ProducesResponseType(typeof(ProblemDetails), (int)HttpStatusCode.BadRequest)]
    [ProducesResponseType(typeof(ProblemDetails), (int)HttpStatusCode.NotFound)]
    [ProducesResponseType(typeof(ProblemDetails), (int)HttpStatusCode.InternalServerError)]
    [ProducesResponseType(typeof(GetArticlesResponse), (int)HttpStatusCode.OK)]
    private static async Task<GetArticlesResponse> GetArticles(
        [AsParameters] GetArticlesRequest request,
        [FromQuery] string? textFilter,
        //[FromQuery] TagFilter[]? tagFilter,
        [FromServices] ISender mediator, CancellationToken ct = default)
    {
        var result =
            await mediator.Send(
                new GetArticlesQuery
                {
                    TextFilter = textFilter,
                    //TagFilter = tagFilter,
                    Offset = request.Offset,
                    Limit = request.Limit
                }, ct);

        return result;
    }

    [ProducesResponseType(typeof(ProblemDetails), (int)HttpStatusCode.BadRequest)]
    [ProducesResponseType(typeof(ProblemDetails), (int)HttpStatusCode.InternalServerError)]
    [ProducesResponseType(typeof(ProblemDetails), (int)HttpStatusCode.NotFound)]
    [ProducesResponseType(typeof(DeleteArticleResponse), (int)HttpStatusCode.OK)]
    private static async Task<DeleteArticleResponse> DeleteArticle([FromRoute] Guid id, [FromServices] ISender mediator,
        CancellationToken ct = default)
    {
        var result = await mediator.Send(new DeleteArticleCommand { Id = id }, ct);
        return result;
    }
}
