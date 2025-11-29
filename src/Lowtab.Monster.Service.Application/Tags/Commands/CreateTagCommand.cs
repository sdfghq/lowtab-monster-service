using Lowtab.Monster.Service.Contracts.Tags.CreateTag;
using Mediator;

namespace Lowtab.Monster.Service.Application.Tags.Commands;

/// <inheritdoc cref="CreateTagRequest" />
public record CreateTagCommand : CreateTagRequest, ICommand<CreateTagResponse>;
