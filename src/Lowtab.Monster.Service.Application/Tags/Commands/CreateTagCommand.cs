using Mediator;
using Lowtab.Monster.Service.Contracts.Tags.CreateTag;

namespace Lowtab.Monster.Service.Application.Tags.Commands;

/// <inheritdoc cref="CreateTagRequest"/>
public record CreateTagCommand : CreateTagRequest, ICommand<CreateTagResponse>;
