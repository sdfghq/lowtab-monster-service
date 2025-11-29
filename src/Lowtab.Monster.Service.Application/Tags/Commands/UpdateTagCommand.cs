using Lowtab.Monster.Service.Contracts.GroupTags;
using Mediator;
using Lowtab.Monster.Service.Contracts.Tags.UpdateTag;

namespace Lowtab.Monster.Service.Application.Tags.Commands;

/// <inheritdoc cref="UpdateTagRequest"/>
public record UpdateTagCommand : UpdateTagRequest, ICommand<UpdateTagResponse>
{
    public required string Id { get; init; }

    public required GroupTagEnum Group { get; init; }
}
