using Lowtab.Monster.Service.Contracts.Tags.Common;
using Lowtab.Monster.Service.Contracts.Tags.GetTags;
using Mediator;

namespace Lowtab.Monster.Service.Application.Tags.Queryes;

/// <summary>
///     Запрос для получения объекта <see cref="TagBase" />
/// </summary>
public record GetTagsQuery : GetTagsRequest, IQuery<GetTagsResponse>
{
    public IReadOnlyCollection<GroupTagEnum>? GroupsFilter { get; init; }

    public string? IdFilter { get; init; }
}
