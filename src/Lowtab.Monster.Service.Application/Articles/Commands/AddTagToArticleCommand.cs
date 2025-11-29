using Mediator;
using Lowtab.Monster.Service.Contracts.Articles.AddTagToArticle;
using Lowtab.Monster.Service.Contracts.GroupTags;

namespace Lowtab.Monster.Service.Application.Articles.Commands;

public record AddTagToArticleCommand : ICommand<AddTagToArticleResponse>
{
    public Guid ArticleId { get; init; }
    public required string TagId { get; init; }
    public GroupTagEnum Group { get; init; }
}
