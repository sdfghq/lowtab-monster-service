using Lowtab.Monster.Service.Contracts.Tags.Common;
using Sdf.Platform.Versioning.Contracts;

namespace Lowtab.Monster.Service.Contracts.Tags;

/// <summary>
///     Маршруты для работы с объектами <see cref="Tag" />
/// </summary>
public static class TagsRoutes
{
    /// <summary>
    ///     Версия API
    /// </summary>
    public const double CurrentVersion = 1.0;

    /// <summary>
    ///     Базовый URL для работы с объектами <see cref="Tag" />
    /// </summary>
    public const string BaseUrl = $"/internal/v{ApiVersionParameter.RouteParameter}/tag";

    /// <summary>
    ///     Маршрут для создания объекта <see cref="TagBase" />
    /// </summary>
    public const string CreateTag = BaseUrl;

    /// <summary>
    ///     Маршрут для получения объекта <see cref="TagBase" />
    /// </summary>
    public const string GetTag = $"{BaseUrl}/{{id}}";

    /// <summary>
    ///     Маршрут для обновления объекта <see cref="TagBase" />
    /// </summary>
    public const string UpdateTag = $"{BaseUrl}/{{id}}";

    /// <summary>
    ///     Маршрут для удаления объекта <see cref="TagBase" />
    /// </summary>
    public const string DeleteTag = $"{BaseUrl}/{{id}}";

    /// <summary>
    ///     Маршрут для получения списка объектов <see cref="TagBase" />
    /// </summary>
    public const string GetTags = $"{BaseUrl}s/{{offset}}/{{limit}}";
}
