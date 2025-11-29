using Mediator;
using Lowtab.Monster.Service.Contracts.Articles.DeleteTagFromArticle;
using Lowtab.Monster.Service.Contracts.GroupTags;

namespace Lowtab.Monster.Service.Application.Articles.Commands;

public record DeleteTagFromArticleCommand : ICommand<DeleteTagFromArticleResponse>
{
    public Guid ArticleId { get; init; }
    public required string TagId { get; init; }
    public GroupTagEnum Group { get; init; }
}
