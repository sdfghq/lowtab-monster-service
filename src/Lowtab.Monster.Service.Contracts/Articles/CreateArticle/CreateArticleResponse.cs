using Lowtab.Monster.Service.Contracts.Articles.Common;

namespace Lowtab.Monster.Service.Contracts.Articles.CreateArticle;

/// <summary>
///     Ответ при создании объекта <see cref="ArticleBase"/>>
/// </summary>
public record CreateArticleResponse
{
    /// <summary>
    ///     Идентификатор
    /// </summary>
    public required Guid Id { get; init; }
}
