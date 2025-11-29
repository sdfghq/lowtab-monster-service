using Mediator;
using Microsoft.Extensions.Logging;
using Lowtab.Monster.Service.Application.Interfaces;
using Lowtab.Monster.Service.Application.GroupTags.Commands;
using Lowtab.Monster.Service.Application.GroupTags.Mappings;
using Lowtab.Monster.Service.Contracts.GroupTags.CreateGroupTag;

namespace Lowtab.Monster.Service.Application.GroupTags.Handlers;

internal class CreateGroupTagHandler
(
    ILogger<CreateGroupTagHandler> logger,
    IDbContext context
) : ICommandHandler<CreateGroupTagCommand, CreateGroupTagResponse>
{
    public async ValueTask<CreateGroupTagResponse> Handle(CreateGroupTagCommand request, CancellationToken ct)
    {
        var newEntity = request.ToEntity();
        logger.LogInformation("Try saving {@Entity} to database", newEntity);

        var result = context.GroupTags.Add(newEntity).Entity;
        await context.SaveChangesAsync(ct);

        logger.LogInformation("Created new {@Object} with {Id}", result, result.Id);
        return new CreateGroupTagResponse { Id = result.Id };
    }
}
