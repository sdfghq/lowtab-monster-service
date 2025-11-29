using Mediator;
using Lowtab.Monster.Service.Application.Common.Exceptions;
using Lowtab.Monster.Service.Application.Interfaces;
using Lowtab.Monster.Service.Application.GroupTags.Commands;
using Lowtab.Monster.Service.Contracts.GroupTags.DeleteGroupTag;

namespace Lowtab.Monster.Service.Application.GroupTags.Handlers;

internal class DeleteGroupTagHandler(IDbContext context) : ICommandHandler<DeleteGroupTagCommand, DeleteGroupTagResponse>
{
    public async ValueTask<DeleteGroupTagResponse> Handle(DeleteGroupTagCommand request,
        CancellationToken ct)
    {
        var obj = await context.GroupTags.FindAsync([request.Id], ct) ??
                  throw new NotFoundException($"Не нашел объект с идентификатором {request.Id}");

        context.GroupTags.Remove(obj);
        await context.SaveChangesAsync(ct);
        return new DeleteGroupTagResponse();
    }
}
