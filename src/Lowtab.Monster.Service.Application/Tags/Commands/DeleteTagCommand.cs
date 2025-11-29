using Lowtab.Monster.Service.Contracts.Tags.Common;
using Lowtab.Monster.Service.Contracts.Tags.DeleteTag;
using Mediator;

namespace Lowtab.Monster.Service.Application.Tags.Commands;

/// <summary>
///     Запрос для удаления объекта <see cref="TagBase" />
/// </summary>
public record DeleteTagCommand : ICommand<DeleteTagResponse>
{
    public required TagId Id { get; init; }
}
