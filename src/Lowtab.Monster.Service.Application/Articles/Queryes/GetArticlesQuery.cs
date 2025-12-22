using Lowtab.Monster.Service.Contracts.Articles.Common;
using Lowtab.Monster.Service.Contracts.Articles.GetArticles;
using Lowtab.Monster.Service.Contracts.Tags.Common;
using Mediator;

namespace Lowtab.Monster.Service.Application.Articles.Queryes;

/// <summary>
///     Запрос для получения объекта <see cref="ArticleBase" />
/// </summary>
public record GetArticlesQuery : GetArticlesRequest, IQuery<GetArticlesResponse>
{
    /// <summary>
    ///     Фильтр по тегам, ищет вхождение
    /// </summary>
    public IReadOnlyCollection<TagIdFilter>? TagFilter { get; init; }

    /// <summary>
    ///     Фильтр по тексту в заголовке или описании
    /// </summary>
    public string? TextFilter { get; init; }
}
