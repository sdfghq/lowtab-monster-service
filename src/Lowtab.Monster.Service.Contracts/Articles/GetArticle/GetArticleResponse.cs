using Lowtab.Monster.Service.Contracts.Articles.Common;
using Lowtab.Monster.Service.Contracts.Tags.Common;

namespace Lowtab.Monster.Service.Contracts.Articles.GetArticle;

/// <summary>
///     Ответ на запрос получения информации об объекте <see cref="Article" />
/// </summary>
public record GetArticleResponse : Article
{
    /// <summary>
    ///     Список тегов
    /// </summary>
    public IReadOnlyCollection<Tag> Tags { get; set; } = [];
}
