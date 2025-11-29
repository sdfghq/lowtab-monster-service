using Mediator;
using Lowtab.Monster.Service.Contracts.GroupTags.Common;
using Lowtab.Monster.Service.Contracts.GroupTags.GetGroupTag;

namespace Lowtab.Monster.Service.Application.GroupTags.Queryes;

/// <summary>
///     Запрос для получения списка объектов <see cref="GroupTagBase"/>
/// </summary>
public record GetGroupTagQuery : IQuery<GetGroupTagResponse>
{
    public required Guid Id { get; init; }
}
