using Sdf.Platform.Versioning.Contracts;
using Lowtab.Monster.Service.Contracts.GroupTags.Common;

namespace Lowtab.Monster.Service.Contracts.GroupTags;

/// <summary>
///     Маршруты для работы с объектами <see cref="GroupTag"/>
/// </summary>
public static class GroupTagsRoutes
{
    /// <summary>
    ///     Версия API
    /// </summary>
    public const double CurrentVersion = 1.0;

    /// <summary>
    ///     Базовый URL для работы с объектами <see cref="GroupTag"/>
    /// </summary>
    public const string BaseUrl = $"/internal/v{ApiVersionParameter.RouteParameter}/group-tag";

    /// <summary>
    ///     Маршрут для создания объекта <see cref="GroupTagBase"/>
    /// </summary>
    public const string CreateGroupTag = BaseUrl;

    /// <summary>
    ///     Маршрут для получения объекта <see cref="GroupTagBase"/>
    /// </summary>
    public const string GetGroupTag = $"{BaseUrl}/{{id}}";

    /// <summary>
    ///     Маршрут для обновления объекта <see cref="GroupTagBase"/>
    /// </summary>
    public const string UpdateGroupTag = $"{BaseUrl}/{{id}}";

    /// <summary>
    ///     Маршрут для удаления объекта <see cref="GroupTagBase"/>
    /// </summary>
    public const string DeleteGroupTag = $"{BaseUrl}/{{id}}";

    /// <summary>
    ///     Маршрут для получения списка объектов <see cref="GroupTagBase"/>
    /// </summary>
    public const string GetGroupTags = $"{BaseUrl}/{{offset}}/{{limit}}";
}
