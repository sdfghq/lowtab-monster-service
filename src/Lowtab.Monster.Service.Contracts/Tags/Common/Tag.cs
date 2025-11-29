namespace Lowtab.Monster.Service.Contracts.Tags.Common;

/// <inheritdoc cref="TagBase"/>
public abstract record Tag : TagBase
{
    /// <summary>
    ///     Идентификатор
    /// </summary>
    public required string Id { get; set; }
}
