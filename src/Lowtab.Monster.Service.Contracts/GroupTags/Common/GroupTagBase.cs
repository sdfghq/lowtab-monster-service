namespace Lowtab.Monster.Service.Contracts.GroupTags.Common;

/// <summary>
///     Объект GroupTags
/// </summary>
public abstract record GroupTagBase
{
    /// <summary>
    ///     Наименование
    /// </summary>
    public required string Description { get; init; }
}
