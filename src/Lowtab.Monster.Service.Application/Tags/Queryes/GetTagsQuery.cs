using Lowtab.Monster.Service.Contracts.Tags.Common;
using Lowtab.Monster.Service.Contracts.Tags.GetTags;
using Mediator;

namespace Lowtab.Monster.Service.Application.Tags.Queryes;

/// <summary>
///     Запрос для получения объекта <see cref="TagBase" />
/// </summary>
public record GetTagsQuery : GetTagsRequest, IQuery<GetTagsResponse>
{
    public IReadOnlyCollection<TagIdFilter>? IdFilter { get; init; }
}
