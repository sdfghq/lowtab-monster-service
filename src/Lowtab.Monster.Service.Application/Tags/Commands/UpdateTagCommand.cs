using Lowtab.Monster.Service.Contracts.Tags.Common;
using Lowtab.Monster.Service.Contracts.Tags.UpdateTag;
using Mediator;

namespace Lowtab.Monster.Service.Application.Tags.Commands;

/// <inheritdoc cref="UpdateTagRequest" />
public record UpdateTagCommand : UpdateTagRequest, ICommand<UpdateTagResponse>
{
    public required TagId Id { get; init; }
}
