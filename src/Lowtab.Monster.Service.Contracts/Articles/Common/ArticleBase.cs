namespace Lowtab.Monster.Service.Contracts.Articles.Common;

/// <summary>
///     Объект Articles
/// </summary>
public abstract record ArticleBase
{
    /// <summary>
    ///     Наименование
    /// </summary>
    public required string Name { get; init; }
}
