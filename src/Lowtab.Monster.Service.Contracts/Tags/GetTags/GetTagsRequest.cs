using Lowtab.Monster.Service.Contracts.Tags.Common;

namespace Lowtab.Monster.Service.Contracts.Tags.GetTags;

/// <summary>
///     Запрос получения списка объектов <see cref="Tag"/>
/// </summary>
public record GetTagsRequest
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
