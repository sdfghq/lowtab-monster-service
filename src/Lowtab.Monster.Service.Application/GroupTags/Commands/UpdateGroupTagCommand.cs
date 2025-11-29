using Mediator;
using Lowtab.Monster.Service.Contracts.GroupTags.UpdateGroupTag;

namespace Lowtab.Monster.Service.Application.GroupTags.Commands;

/// <inheritdoc cref="UpdateGroupTagRequest"/>
public record UpdateGroupTagCommand : UpdateGroupTagRequest, ICommand<UpdateGroupTagResponse>
{
    public required Guid Id { get; init; }
}
