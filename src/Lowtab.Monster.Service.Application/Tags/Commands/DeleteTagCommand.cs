using Lowtab.Monster.Service.Contracts.GroupTags;
using Mediator;
using Lowtab.Monster.Service.Contracts.Tags.Common;
using Lowtab.Monster.Service.Contracts.Tags.DeleteTag;

namespace Lowtab.Monster.Service.Application.Tags.Commands;

/// <summary>
///     Запрос для удаления объекта <see cref="TagBase"/>
/// </summary>
public record DeleteTagCommand : ICommand<DeleteTagResponse>
{
    public required string Id { get; init; }

    public required GroupTagEnum Group { get; init; }
}
