using Lowtab.Monster.Service.Contracts.Articles.AddTagToArticle;
using Lowtab.Monster.Service.Contracts.Tags.Common;
using Mediator;

namespace Lowtab.Monster.Service.Application.Articles.Commands;

public record AddTagToArticleCommand : ICommand<AddTagToArticleResponse>
{
    public Guid ArticleId { get; init; }
    public required TagId TagId { get; init; }
}
