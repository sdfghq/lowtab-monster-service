namespace Lowtab.Monster.Service.Contracts.Tags.Common;

/// <summary>
///     Объект Tags
/// </summary>
public abstract record TagBase
{
    /// <summary>
    ///     Описание тега
    /// </summary>
    public string? Description { get; set; }
}
