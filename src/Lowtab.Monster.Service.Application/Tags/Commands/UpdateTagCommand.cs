using Mediator;
using Lowtab.Monster.Service.Contracts.Tags.UpdateTag;

namespace Lowtab.Monster.Service.Application.Tags.Commands;

/// <inheritdoc cref="UpdateTagRequest"/>
public record UpdateTagCommand : UpdateTagRequest, ICommand<UpdateTagResponse>
{
    public required Guid Id { get; init; }
}
