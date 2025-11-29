using Lowtab.Monster.Service.Contracts.Articles.Common;
using Lowtab.Monster.Service.Contracts.Articles.GetArticle;
using Mediator;

namespace Lowtab.Monster.Service.Application.Articles.Queryes;

/// <summary>
///     Запрос для получения списка объектов <see cref="ArticleBase" />
/// </summary>
public record GetArticleQuery : IQuery<GetArticleResponse>
{
    public required Guid Id { get; init; }
}
