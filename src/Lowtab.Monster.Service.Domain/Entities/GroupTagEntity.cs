using Sdf.Platform.EntityFrameworkCore.Abstractions;
using Lowtab.Monster.Service.Contracts.GroupTags.Common;

namespace Lowtab.Monster.Service.Domain.Entities;

/// <summary>
///     Модель объекта <inheritdoc cref="GroupTag"/> в базе данных
/// </summary>
public class GroupTagEntity : ITrackedTime
{
    /// <summary>
    ///     Идентификатор
    /// </summary>
    public GroupTagEnum Id { get; set; }

    /// <summary>
    ///     Описание
    /// </summary>
    public required string Description { get; set; }

    public DateTimeOffset CreatedAt { get; set; }

    public DateTimeOffset UpdatedAt { get; set; }

    /// <summary>
    ///     Список тегов
    /// </summary>
    public ICollection<TagEntity>? Tags { get; set; }
}
