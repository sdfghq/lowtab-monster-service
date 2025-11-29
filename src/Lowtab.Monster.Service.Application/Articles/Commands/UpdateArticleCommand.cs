using Mediator;
using Lowtab.Monster.Service.Contracts.Articles.UpdateArticle;

namespace Lowtab.Monster.Service.Application.Articles.Commands;

/// <inheritdoc cref="UpdateArticleRequest"/>
public record UpdateArticleCommand : UpdateArticleRequest, ICommand<UpdateArticleResponse>
{
    public required Guid Id { get; init; }
}
