using Lowtab.Monster.Service.Application.Articles.Commands;
using Lowtab.Monster.Service.Application.Common.Exceptions;
using Lowtab.Monster.Service.Application.Interfaces;
using Lowtab.Monster.Service.Contracts.Articles.AddTagToArticle;
using Mediator;
using Microsoft.EntityFrameworkCore;

namespace Lowtab.Monster.Service.Application.Articles.Handlers;

internal class AddTagToArticleHandler(IDbContext context)
    : ICommandHandler<AddTagToArticleCommand, AddTagToArticleResponse>
{
    public async ValueTask<AddTagToArticleResponse> Handle(AddTagToArticleCommand request, CancellationToken ct)
    {
        var article = await context.Articles
                          .Include(x => x.Tags)
                          .FirstOrDefaultAsync(x => x.Id == request.ArticleId, ct)
                      ?? throw new NotFoundException($"Article with id {request.ArticleId} not found");

        var tag = await context.Tags.FirstOrDefaultAsync(
                      x => x.Id == request.TagId.Id && x.Group == request.TagId.Group, ct)
                  ?? throw new NotFoundException($"Tag with id {request.TagId} not found");

        if (article.Tags.Any(t => t.Id == tag.Id && t.Group == tag.Group))
        {
            return new AddTagToArticleResponse();
        }

        article.Tags.Add(tag);
        await context.SaveChangesAsync(ct);

        return new AddTagToArticleResponse();
    }
}
