using Mediator;
using Lowtab.Monster.Service.Contracts.GroupTags.CreateGroupTag;

namespace Lowtab.Monster.Service.Application.GroupTags.Commands;

/// <inheritdoc cref="CreateGroupTagRequest"/>
public record CreateGroupTagCommand : CreateGroupTagRequest, ICommand<CreateGroupTagResponse>;
