using Lowtab.Monster.Service.Application.Common.Exceptions;
using Lowtab.Monster.Service.Application.Interfaces;
using Lowtab.Monster.Service.Application.Tags.Commands;
using Lowtab.Monster.Service.Application.Tags.Mappings;
using Lowtab.Monster.Service.Contracts.Tags.UpdateTag;
using Mediator;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Lowtab.Monster.Service.Application.Tags.Handlers;

internal class UpdateTagHandler(
    ILogger<UpdateTagHandler> logger,
    IDbContext context
) : ICommandHandler<UpdateTagCommand, UpdateTagResponse>
{
    public async ValueTask<UpdateTagResponse> Handle(UpdateTagCommand request, CancellationToken ct)
    {
        var newEntity = request.ToEntity();

        logger.LogInformation("Try update {@Entity} in database", newEntity);
        var updatedEntity =
            await context.Tags.FirstOrDefaultAsync(x => x.Id == request.Id, ct) ??
            throw new NotFoundException($"Не нашел объект с идентификатором {newEntity.Id}");

        newEntity.CopyTo(updatedEntity);

        await context.SaveChangesAsync(ct);
        return new UpdateTagResponse();
    }
}
