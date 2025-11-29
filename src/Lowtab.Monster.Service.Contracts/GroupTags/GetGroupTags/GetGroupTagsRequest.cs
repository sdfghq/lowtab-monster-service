using Lowtab.Monster.Service.Contracts.GroupTags.Common;

namespace Lowtab.Monster.Service.Contracts.GroupTags.GetGroupTags;

/// <summary>
///     Запрос получения списка объектов <see cref="GroupTag"/>
/// </summary>
public record GetGroupTagsRequest
{
    /// <summary>
    ///     Фильтр по имени
    /// </summary>
    public string? NameFilter { get; set; }

    /// <summary>
    ///     Отступ от начала списка
    /// </summary>
    public required int Offset { get; init; }

    /// <summary>
    ///      Количество элементов в списке
    /// </summary>
    public required int Limit { get; init; }
}
