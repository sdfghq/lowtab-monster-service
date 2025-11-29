namespace Lowtab.Monster.Service.Contracts.GroupTags.Common;

/// <inheritdoc cref="GroupTagBase"/>
public abstract record GroupTag : GroupTagBase
{
    /// <summary>
    ///     Идентификатор
    /// </summary>
    public required GroupTagEnum Id { get; set; }
}
