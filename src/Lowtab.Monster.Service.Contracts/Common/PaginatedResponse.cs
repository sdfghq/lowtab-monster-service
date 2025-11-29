namespace Lowtab.Monster.Service.Contracts.Common;

/// <summary>
///     Пагинированный список
/// </summary>
/// <typeparam name="T"></typeparam>
public abstract record PaginatedResponse<T>
{
    /// <summary>
    ///     Элементы
    /// </summary>
    public required IReadOnlyCollection<T> Items { get; init; }

    /// <summary>
    ///     Найдено элементов
    /// </summary>
    public required int TotalFound { get; init; }

    /// <summary>
    ///     Всего элементов
    /// </summary>
    public required int TotalCount { get; init; }
}
