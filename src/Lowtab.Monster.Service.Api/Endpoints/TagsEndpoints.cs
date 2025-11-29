using System.Net;
using Mediator;
using Microsoft.AspNetCore.Mvc;
using Lowtab.Monster.Service.Application.Tags.Commands;
using Lowtab.Monster.Service.Application.Tags.Queryes;
using Lowtab.Monster.Service.Contracts.GroupTags;
using Lowtab.Monster.Service.Contracts.Tags;
using Lowtab.Monster.Service.Contracts.Tags.CreateTag;
using Lowtab.Monster.Service.Contracts.Tags.DeleteTag;
using Lowtab.Monster.Service.Contracts.Tags.GetTag;
using Lowtab.Monster.Service.Contracts.Tags.GetTags;
using Lowtab.Monster.Service.Contracts.Tags.UpdateTag;

namespace Lowtab.Monster.Service.Api.Endpoints;

internal static class TagsEndpoints
{
    private const string Tag = "Tags internal methods";

    public static WebApplication MapTagEndpoints(this WebApplication app)
    {
        var api = app
            .NewVersionedApi(Tag)
            .HasApiVersion(1.0);

        api.MapPost(TagsRoutes.CreateTag, CreateTag);
        api.MapGet(TagsRoutes.GetTag, GetTag);
        api.MapGet(TagsRoutes.GetTags, GetTags);
        api.MapPut(TagsRoutes.UpdateTag, UpdateTag);
        api.MapDelete(TagsRoutes.DeleteTag, DeleteTag);

        return app;
    }

    [ProducesResponseType(typeof(ProblemDetails), (int)HttpStatusCode.BadRequest)]
    [ProducesResponseType(typeof(ProblemDetails), (int)HttpStatusCode.InternalServerError)]
    [ProducesResponseType(typeof(CreateTagResponse), (int)HttpStatusCode.OK)]
    private static async Task<CreateTagResponse> CreateTag([FromBody] CreateTagRequest request,
        [FromServices] ISender mediator, CancellationToken ct = default)
    {
        var result =
            await mediator.Send(
                new CreateTagCommand { Description = request.Description, Id = request.Id, Group = request.Group }, ct);

        return result;
    }

    [ProducesResponseType(typeof(ProblemDetails), (int)HttpStatusCode.BadRequest)]
    [ProducesResponseType(typeof(ProblemDetails), (int)HttpStatusCode.InternalServerError)]
    [ProducesResponseType(typeof(UpdateTagResponse), (int)HttpStatusCode.OK)]
    private static async Task<UpdateTagResponse> UpdateTag(
        [FromRoute] string id, [FromRoute] GroupTagEnum group, [FromBody] UpdateTagRequest request, [FromServices] ISender mediator, CancellationToken ct = default)
    {
        var result =
            await mediator.Send(new UpdateTagCommand { Id = id, Group = group, Description = request.Description }, ct);
        return result;
    }

    [ProducesResponseType(typeof(ProblemDetails), (int)HttpStatusCode.BadRequest)]
    [ProducesResponseType(typeof(ProblemDetails), (int)HttpStatusCode.NotFound)]
    [ProducesResponseType(typeof(ProblemDetails), (int)HttpStatusCode.InternalServerError)]
    [ProducesResponseType(typeof(CreateTagResponse), (int)HttpStatusCode.OK)]
    private static async Task<GetTagResponse> GetTag([FromRoute] string id, [FromRoute] GroupTagEnum group, [FromServices] ISender mediator,
        CancellationToken ct = default)
    {
        var result = await mediator.Send(new GetTagQuery { Id = id, Group = group }, ct);
        return result;
    }

    [ProducesResponseType(typeof(ProblemDetails), (int)HttpStatusCode.BadRequest)]
    [ProducesResponseType(typeof(ProblemDetails), (int)HttpStatusCode.NotFound)]
    [ProducesResponseType(typeof(ProblemDetails), (int)HttpStatusCode.InternalServerError)]
    [ProducesResponseType(typeof(GetTagsResponse), (int)HttpStatusCode.OK)]
    private static async Task<GetTagsResponse> GetTags([AsParameters] GetTagsRequest request,
        [FromServices] ISender mediator, CancellationToken ct = default)
    {
        var result =
            await mediator.Send(
                new GetTagsQuery
                {
                    NameFilter = request.NameFilter,
                    Offset = request.Offset,
                    Limit = request.Limit
                }, ct);
        return result;
    }

    [ProducesResponseType(typeof(ProblemDetails), (int)HttpStatusCode.BadRequest)]
    [ProducesResponseType(typeof(ProblemDetails), (int)HttpStatusCode.InternalServerError)]
    [ProducesResponseType(typeof(ProblemDetails), (int)HttpStatusCode.NotFound)]
    [ProducesResponseType(typeof(DeleteTagResponse), (int)HttpStatusCode.OK)]
    private static async Task<DeleteTagResponse> DeleteTag([FromRoute] string id, [FromRoute] GroupTagEnum group, [FromServices] ISender mediator,
        CancellationToken ct = default)
    {
        var result = await mediator.Send(new DeleteTagCommand { Id = id, Group = group }, ct);
        return result;
    }
}
