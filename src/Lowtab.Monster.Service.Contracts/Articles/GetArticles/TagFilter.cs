using Lowtab.Monster.Service.Contracts.GroupTags;

namespace Lowtab.Monster.Service.Contracts.Articles.GetArticles;

/// <summary>
///     Фильтр по тегу
/// </summary>
public record TagFilter
{
    /// <summary>
    ///     Идентификатор тега
    /// </summary>
    public string? Tag { get; init; }

    /// <summary>
    ///     Группа тега
    /// </summary>
    public GroupTagEnum? Group { get; init; }
}
