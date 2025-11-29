using Mediator;
using Lowtab.Monster.Service.Application.Common.Exceptions;
using Lowtab.Monster.Service.Application.Interfaces;
using Lowtab.Monster.Service.Application.Tags.Commands;
using Lowtab.Monster.Service.Contracts.Tags.DeleteTag;

namespace Lowtab.Monster.Service.Application.Tags.Handlers;

internal class DeleteTagHandler(IDbContext context) : ICommandHandler<DeleteTagCommand, DeleteTagResponse>
{
    public async ValueTask<DeleteTagResponse> Handle(DeleteTagCommand request,
        CancellationToken ct)
    {
        var obj = await context.Tags.FindAsync([request.Id], ct) ??
                  throw new NotFoundException($"Не нашел объект с идентификатором {request.Id}");

        context.Tags.Remove(obj);
        await context.SaveChangesAsync(ct);
        return new DeleteTagResponse();
    }
}
