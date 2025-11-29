using Mediator;
using Lowtab.Monster.Service.Contracts.Tags.Common;
using Lowtab.Monster.Service.Contracts.Tags.GetTag;

namespace Lowtab.Monster.Service.Application.Tags.Queryes;

/// <summary>
///     Запрос для получения списка объектов <see cref="TagBase"/>
/// </summary>
public record GetTagQuery : IQuery<GetTagResponse>
{
    public required Guid Id { get; init; }
}
