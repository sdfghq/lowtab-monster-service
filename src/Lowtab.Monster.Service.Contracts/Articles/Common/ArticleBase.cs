namespace Lowtab.Monster.Service.Contracts.Articles.Common;

/// <summary>
///     Объект Articles
/// </summary>
public abstract record ArticleBase
{
    /// <summary>
    ///     Наименование
    /// </summary>
    public required string Title { get; init; }

    /// <summary>
    ///     Описание
    /// </summary>
    public required string Body { get; set; }

    /// <summary>
    ///     Картинка на превью
    /// </summary>
    public Uri? PreviewImageUrl { get; set; }
}
