using Lowtab.Monster.Service.Contracts.Articles.Common;

namespace Lowtab.Monster.Service.Contracts.Articles.GetArticles;

/// <summary>
///     Запрос получения списка объектов <see cref="Article" />
/// </summary>
public record GetArticlesRequest
{
    /// <summary>
    ///     Отступ от начала списка
    /// </summary>
    public required int Offset { get; init; }

    /// <summary>
    ///     Количество элементов в списке
    /// </summary>
    public required int Limit { get; init; }
}
