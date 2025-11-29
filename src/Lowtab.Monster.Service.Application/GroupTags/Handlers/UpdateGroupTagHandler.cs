using Mediator;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Lowtab.Monster.Service.Application.Common.Exceptions;
using Lowtab.Monster.Service.Application.Interfaces;
using Lowtab.Monster.Service.Application.GroupTags.Commands;
using Lowtab.Monster.Service.Application.GroupTags.Mappings;
using Lowtab.Monster.Service.Contracts.GroupTags.UpdateGroupTag;

namespace Lowtab.Monster.Service.Application.GroupTags.Handlers;

internal class UpdateGroupTagHandler
(
    ILogger<UpdateGroupTagHandler> logger,
    IDbContext context
) : ICommandHandler<UpdateGroupTagCommand, UpdateGroupTagResponse>
{
    public async ValueTask<UpdateGroupTagResponse> Handle(UpdateGroupTagCommand request, CancellationToken ct)
    {
        var newEntity = request.ToEntity();

        logger.LogInformation("Try update {@Entity} in database", newEntity);
        var updatedEntity = await context.GroupTags.FirstOrDefaultAsync(x => x.Id == newEntity.Id, ct) ??
                            throw new NotFoundException($"Не нашел объект с идентификатором {newEntity.Id}");

        newEntity.CopyTo(updatedEntity);

        await context.SaveChangesAsync(ct);
        return new UpdateGroupTagResponse();
    }
}
