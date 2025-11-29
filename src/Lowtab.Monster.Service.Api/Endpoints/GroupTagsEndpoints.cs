using System.Net;
using Mediator;
using Microsoft.AspNetCore.Mvc;
using Lowtab.Monster.Service.Application.GroupTags.Commands;
using Lowtab.Monster.Service.Application.GroupTags.Queryes;
using Lowtab.Monster.Service.Contracts.GroupTags;
using Lowtab.Monster.Service.Contracts.GroupTags.CreateGroupTag;
using Lowtab.Monster.Service.Contracts.GroupTags.DeleteGroupTag;
using Lowtab.Monster.Service.Contracts.GroupTags.GetGroupTag;
using Lowtab.Monster.Service.Contracts.GroupTags.GetGroupTags;
using Lowtab.Monster.Service.Contracts.GroupTags.UpdateGroupTag;

namespace Lowtab.Monster.Service.Api.Endpoints;

internal static class GroupTagsEndpoints
{
    private const string Tag = "GroupTags internal methods";

    public static WebApplication MapGroupTagEndpoints(this WebApplication app)
    {
        var api = app
            .NewVersionedApi(Tag)
            .HasApiVersion(1.0);

        api.MapPost(GroupTagsRoutes.CreateGroupTag, CreateGroupTag);
        api.MapGet(GroupTagsRoutes.GetGroupTag, GetGroupTag);
        api.MapGet(GroupTagsRoutes.GetGroupTags, GetGroupTags);
        api.MapPut(GroupTagsRoutes.UpdateGroupTag, UpdateGroupTag);
        api.MapDelete(GroupTagsRoutes.DeleteGroupTag, DeleteGroupTag);

        return app;
    }

    [ProducesResponseType(typeof(ProblemDetails), (int)HttpStatusCode.BadRequest)]
    [ProducesResponseType(typeof(ProblemDetails), (int)HttpStatusCode.InternalServerError)]
    [ProducesResponseType(typeof(CreateGroupTagResponse), (int)HttpStatusCode.OK)]
    private static async Task<CreateGroupTagResponse> CreateGroupTag([FromBody] CreateGroupTagRequest request,
        [FromServices] ISender mediator, CancellationToken ct = default)
    {
        var result = await mediator.Send(new CreateGroupTagCommand { Description = request.Description }, ct);
        return result;
    }

    [ProducesResponseType(typeof(ProblemDetails), (int)HttpStatusCode.BadRequest)]
    [ProducesResponseType(typeof(ProblemDetails), (int)HttpStatusCode.InternalServerError)]
    [ProducesResponseType(typeof(UpdateGroupTagResponse), (int)HttpStatusCode.OK)]
    private static async Task<UpdateGroupTagResponse> UpdateGroupTag(
        [FromRoute] Guid id, [FromBody] UpdateGroupTagRequest request, [FromServices] ISender mediator, CancellationToken ct = default)
    {
        var result = await mediator.Send(new UpdateGroupTagCommand { Description = request.Description, Id = id }, ct);
        return result;
    }

    [ProducesResponseType(typeof(ProblemDetails), (int)HttpStatusCode.BadRequest)]
    [ProducesResponseType(typeof(ProblemDetails), (int)HttpStatusCode.NotFound)]
    [ProducesResponseType(typeof(ProblemDetails), (int)HttpStatusCode.InternalServerError)]
    [ProducesResponseType(typeof(CreateGroupTagResponse), (int)HttpStatusCode.OK)]
    private static async Task<GetGroupTagResponse> GetGroupTag([FromRoute] Guid id, [FromServices] ISender mediator,
        CancellationToken ct = default)
    {
        var result = await mediator.Send(new GetGroupTagQuery { Id = id }, ct);
        return result;
    }

    [ProducesResponseType(typeof(ProblemDetails), (int)HttpStatusCode.BadRequest)]
    [ProducesResponseType(typeof(ProblemDetails), (int)HttpStatusCode.NotFound)]
    [ProducesResponseType(typeof(ProblemDetails), (int)HttpStatusCode.InternalServerError)]
    [ProducesResponseType(typeof(GetGroupTagsResponse), (int)HttpStatusCode.OK)]
    private static async Task<GetGroupTagsResponse> GetGroupTags([AsParameters] GetGroupTagsRequest request,
        [FromServices] ISender mediator, CancellationToken ct = default)
    {
        var result =
            await mediator.Send(
                new GetGroupTagsQuery
                {
                    NameFilter = request.NameFilter, Offset = request.Offset, Limit = request.Limit
                }, ct);
        return result;
    }

    [ProducesResponseType(typeof(ProblemDetails), (int)HttpStatusCode.BadRequest)]
    [ProducesResponseType(typeof(ProblemDetails), (int)HttpStatusCode.InternalServerError)]
    [ProducesResponseType(typeof(ProblemDetails), (int)HttpStatusCode.NotFound)]
    [ProducesResponseType(typeof(DeleteGroupTagResponse), (int)HttpStatusCode.OK)]
    private static async Task<DeleteGroupTagResponse> DeleteGroupTag([FromRoute] Guid id, [FromServices] ISender mediator,
        CancellationToken ct = default)
    {
        var result = await mediator.Send(new DeleteGroupTagCommand { Id = id }, ct);
        return result;
    }
}
