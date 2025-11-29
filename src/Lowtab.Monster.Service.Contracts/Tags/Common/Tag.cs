using Lowtab.Monster.Service.Contracts.GroupTags;

namespace Lowtab.Monster.Service.Contracts.Tags.Common;

/// <inheritdoc cref="TagBase" />
public record Tag : TagBase
{
    /// <summary>
    ///     Идентификатор тега
    /// </summary>
    public required string Id { get; set; }

    /// <summary>
    ///     Группа
    /// </summary>
    public required GroupTagEnum Group { get; set; }
}
