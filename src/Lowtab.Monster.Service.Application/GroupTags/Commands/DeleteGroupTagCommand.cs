using Mediator;
using Lowtab.Monster.Service.Contracts.GroupTags.Common;
using Lowtab.Monster.Service.Contracts.GroupTags.DeleteGroupTag;

namespace Lowtab.Monster.Service.Application.GroupTags.Commands;

/// <summary>
///     Запрос для удаления объекта <see cref="GroupTagBase"/>
/// </summary>
public record DeleteGroupTagCommand : ICommand<DeleteGroupTagResponse>
{
    public required Guid Id { get; init; }
}
