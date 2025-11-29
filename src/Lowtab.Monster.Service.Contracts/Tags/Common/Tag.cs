namespace Lowtab.Monster.Service.Contracts.Tags.Common;

/// <inheritdoc cref="TagBase" />
public record Tag : TagBase
{
    /// <summary>
    ///     Идентификатор тега
    /// </summary>
    public required TagId Id { get; set; }
}
