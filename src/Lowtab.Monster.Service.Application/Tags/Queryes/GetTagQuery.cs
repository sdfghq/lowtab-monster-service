using Lowtab.Monster.Service.Contracts.GroupTags;
using Lowtab.Monster.Service.Contracts.Tags.Common;
using Lowtab.Monster.Service.Contracts.Tags.GetTag;
using Mediator;

namespace Lowtab.Monster.Service.Application.Tags.Queryes;

/// <summary>
///     Запрос для получения списка объектов <see cref="TagBase" />
/// </summary>
public record GetTagQuery : IQuery<GetTagResponse>
{
    public required string Id { get; init; }

    public required GroupTagEnum Group { get; init; }
}
