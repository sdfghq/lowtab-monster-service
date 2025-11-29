using Sdf.Platform.EntityFrameworkCore.Abstractions;
using Lowtab.Monster.Service.Contracts.Tags.Common;

namespace Lowtab.Monster.Service.Domain.Entities;

/// <summary>
///     Модель объекта <inheritdoc cref="Tag"/> в базе данных
/// </summary>
public class TagEntity : ITrackedTime
{
    /// <summary>
    ///     Идентификатор тега
    /// </summary>
    public required string Id { get; set; }

    /// <summary>
    ///     Колекция групп тегов
    /// </summary>
    public GroupTagEntity Groups { get; set; }

    public DateTimeOffset CreatedAt { get; set; }

    public DateTimeOffset UpdatedAt { get; set; }
}
