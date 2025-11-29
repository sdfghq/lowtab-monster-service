using Lowtab.Monster.Service.Contracts.Articles.Common;
using Lowtab.Monster.Service.Contracts.Articles.GetArticles;
using Mediator;

namespace Lowtab.Monster.Service.Application.Articles.Queryes;

/// <summary>
///     Запрос для получения объекта <see cref="ArticleBase" />
/// </summary>
public record GetArticlesQuery : GetArticlesRequest, IQuery<GetArticlesResponse>
{
    public IReadOnlyCollection<TagFilter>? TagFilter { get; init; }

    public string? TextFilter { get; init; }
}
