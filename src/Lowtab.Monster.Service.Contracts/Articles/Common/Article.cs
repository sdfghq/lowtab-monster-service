namespace Lowtab.Monster.Service.Contracts.Articles.Common;

/// <inheritdoc cref="ArticleBase"/>
public abstract record Article : ArticleBase
{
    /// <summary>
    ///     Идентификатор
    /// </summary>
    public required Guid Id { get; set; }
}
