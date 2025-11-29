using Lowtab.Monster.Service.Contracts.Articles.Common;
using Sdf.Platform.Versioning.Contracts;

namespace Lowtab.Monster.Service.Contracts.Articles;

/// <summary>
///     Маршруты для работы с объектами <see cref="Article" />
/// </summary>
public static class ArticlesRoutes
{
    /// <summary>
    ///     Версия API
    /// </summary>
    public const double CurrentVersion = 1.0;

    /// <summary>
    ///     Базовый URL для работы с объектами <see cref="Article" />
    /// </summary>
    public const string BaseUrl = $"/internal/v{ApiVersionParameter.RouteParameter}/article";

    /// <summary>
    ///     Маршрут для создания объекта <see cref="ArticleBase" />
    /// </summary>
    public const string CreateArticle = BaseUrl;

    /// <summary>
    ///     Маршрут для получения объекта <see cref="ArticleBase" />
    /// </summary>
    public const string GetArticle = $"{BaseUrl}/{{id}}";

    /// <summary>
    ///     Маршрут для обновления объекта <see cref="ArticleBase" />
    /// </summary>
    public const string UpdateArticle = $"{BaseUrl}/{{id}}";

    /// <summary>
    ///     Маршрут для удаления объекта <see cref="ArticleBase" />
    /// </summary>
    public const string DeleteArticle = $"{BaseUrl}/{{id}}";

    /// <summary>
    ///     Маршрут для получения списка объектов <see cref="ArticleBase" />
    /// </summary>
    public const string GetArticles = $"{BaseUrl}/{{offset}}/{{limit}}";

    /// <summary>
    ///     Маршрут для добавления тега к статье
    /// </summary>
    public const string AddTagToArticle = $"{BaseUrl}/{{id}}/tag/{{tagId}}/{{group}}";
}
