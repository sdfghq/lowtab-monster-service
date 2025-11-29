using Lowtab.Monster.Service.Contracts.Articles.Common;
using Lowtab.Monster.Service.Contracts.Articles.DeleteArticle;
using Mediator;

namespace Lowtab.Monster.Service.Application.Articles.Commands;

/// <summary>
///     Запрос для удаления объекта <see cref="ArticleBase" />
/// </summary>
public record DeleteArticleCommand : ICommand<DeleteArticleResponse>
{
    public required Guid Id { get; init; }
}
