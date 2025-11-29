using Lowtab.Monster.Service.Contracts.GroupTags.Common;

namespace Lowtab.Monster.Service.Contracts.GroupTags.CreateGroupTag;

/// <summary>
///     Ответ при создании объекта <see cref="GroupTagBase"/>>
/// </summary>
public record CreateGroupTagResponse
{
    /// <summary>
    ///     Идентификатор
    /// </summary>
    public required GroupTagEnum Id { get; init; }
}
