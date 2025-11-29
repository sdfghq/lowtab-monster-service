using Lowtab.Monster.Service.Contracts.Articles.Common;
using Sdf.Platform.EntityFrameworkCore.Abstractions;

namespace Lowtab.Monster.Service.Domain.Entities;

/// <summary>
///     Модель объекта <inheritdoc cref="Article" /> в базе данных
/// </summary>
public class ArticleEntity : ITrackedTime
{
    /// <summary>
    ///     Идентификатор
    /// </summary>
    public Guid Id { get; set; }

    /// <summary>
    ///     Имя
    /// </summary>
    public required string Title { get; set; }

    /// <summary>
    ///     Описание
    /// </summary>
    public required string Body { get; set; }

    /// <summary>
    ///     Картинка на превью
    /// </summary>
    public Uri? PreviewImageUrl { get; set; }

    /// <summary>
    ///     Список тегов
    /// </summary>
    public ICollection<TagEntity> Tags { get; set; }

    public DateTimeOffset CreatedAt { get; set; }

    public DateTimeOffset UpdatedAt { get; set; }
}
