using Lowtab.Monster.Service.Application.Articles.Commands;
using Lowtab.Monster.Service.Application.Common.Exceptions;
using Lowtab.Monster.Service.Application.Interfaces;
using Lowtab.Monster.Service.Contracts.Articles.DeleteTagFromArticle;
using Mediator;
using Microsoft.EntityFrameworkCore;

namespace Lowtab.Monster.Service.Application.Articles.Handlers;

internal class DeleteTagFromArticleHandler(IDbContext context)
    : ICommandHandler<DeleteTagFromArticleCommand, DeleteTagFromArticleResponse>
{
    public async ValueTask<DeleteTagFromArticleResponse> Handle(DeleteTagFromArticleCommand request,
        CancellationToken ct)
    {
        var article = await context.Articles
                          .Include(x => x.Tags)
                          .FirstOrDefaultAsync(x => x.Id == request.ArticleId, ct)
                      ?? throw new NotFoundException($"Article with id {request.ArticleId} not found");

        var tagToRemove =
            await context.Tags.FirstOrDefaultAsync(x => x.Id == request.TagId.Id && x.Group == request.TagId.Group, ct);

        if (tagToRemove == null)
        {
            return new DeleteTagFromArticleResponse();
        }

        article.Tags.Remove(tagToRemove);
        await context.SaveChangesAsync(ct);

        return new DeleteTagFromArticleResponse();
    }
}
