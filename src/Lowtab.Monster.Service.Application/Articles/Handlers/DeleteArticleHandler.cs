using Lowtab.Monster.Service.Application.Articles.Commands;
using Lowtab.Monster.Service.Application.Common.Exceptions;
using Lowtab.Monster.Service.Application.Interfaces;
using Lowtab.Monster.Service.Contracts.Articles.DeleteArticle;
using Mediator;

namespace Lowtab.Monster.Service.Application.Articles.Handlers;

internal class DeleteArticleHandler(IDbContext context) : ICommandHandler<DeleteArticleCommand, DeleteArticleResponse>
{
    public async ValueTask<DeleteArticleResponse> Handle(DeleteArticleCommand request,
        CancellationToken ct)
    {
        var obj = await context.Articles.FindAsync([request.Id], ct) ??
                  throw new NotFoundException($"Не нашел объект с идентификатором {request.Id}");

        context.Articles.Remove(obj);
        await context.SaveChangesAsync(ct);
        return new DeleteArticleResponse();
    }
}
