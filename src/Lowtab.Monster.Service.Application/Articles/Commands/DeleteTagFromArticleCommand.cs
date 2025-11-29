using Lowtab.Monster.Service.Contracts.Articles.DeleteTagFromArticle;
using Lowtab.Monster.Service.Contracts.Tags.Common;
using Mediator;

namespace Lowtab.Monster.Service.Application.Articles.Commands;

public record DeleteTagFromArticleCommand : ICommand<DeleteTagFromArticleResponse>
{
    public Guid ArticleId { get; init; }
    public required TagId TagId { get; init; }
}
