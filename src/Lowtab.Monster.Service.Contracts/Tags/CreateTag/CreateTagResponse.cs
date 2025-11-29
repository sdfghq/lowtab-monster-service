using Lowtab.Monster.Service.Contracts.Tags.Common;

namespace Lowtab.Monster.Service.Contracts.Tags.CreateTag;

/// <summary>
///     Ответ при создании объекта <see cref="TagBase" />>
/// </summary>
public record CreateTagResponse
{
    /// <summary>
    ///     Идентификатор
    /// </summary>
    public required string Id { get; init; }
}
