using Lowtab.Monster.Service.Application.Common.Exceptions;
using Lowtab.Monster.Service.Application.Interfaces;
using Lowtab.Monster.Service.Application.Tags.Commands;
using Lowtab.Monster.Service.Contracts.Tags.DeleteTag;
using Mediator;
using Microsoft.EntityFrameworkCore;

namespace Lowtab.Monster.Service.Application.Tags.Handlers;

internal class DeleteTagHandler(IDbContext context) : ICommandHandler<DeleteTagCommand, DeleteTagResponse>
{
    public async ValueTask<DeleteTagResponse> Handle(DeleteTagCommand request,
        CancellationToken ct)
    {
        var entity = await context.Tags.FirstOrDefaultAsync(x => x.Id == request.Id, ct) ??
                     throw new NotFoundException($"Не нашел объект с идентификатором {request.Id}");

        context.Tags.Remove(entity);
        await context.SaveChangesAsync(ct);
        return new DeleteTagResponse();
    }
}
