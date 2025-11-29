using Lowtab.Monster.Service.Contracts.Tags.Common;
using Sdf.Platform.EntityFrameworkCore.Abstractions;

namespace Lowtab.Monster.Service.Domain.Entities;

/// <summary>
///     Модель объекта <inheritdoc cref="Tag" /> в базе данных
/// </summary>
public class TagEntity : ITrackedTime
{
    /// <summary>
    ///     Идентификатор тега
    /// </summary>
    public required TagId Id { get; set; }

    /// <summary>
    ///     Описание
    /// </summary>
    public string? Description { get; set; }

    public DateTimeOffset CreatedAt { get; set; }

    public DateTimeOffset UpdatedAt { get; set; }
}
