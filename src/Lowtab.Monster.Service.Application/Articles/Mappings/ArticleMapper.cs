using Riok.Mapperly.Abstractions;
using Lowtab.Monster.Service.Application.Articles.Commands;
using Lowtab.Monster.Service.Contracts.Articles.GetArticle;
using Lowtab.Monster.Service.Domain.Entities;

namespace Lowtab.Monster.Service.Application.Articles.Mappings;

[Mapper(UseDeepCloning = true, EnumMappingStrategy = EnumMappingStrategy.ByName)]
internal static partial class ArticleMapper
{
    [MapperIgnoreTarget(nameof(ArticleEntity.Id))]
    [MapperIgnoreTarget(nameof(ArticleEntity.CreatedAt))]
    [MapperIgnoreTarget(nameof(ArticleEntity.UpdatedAt))]
    public static partial ArticleEntity ToEntity(this CreateArticleCommand command);

    [MapperIgnoreTarget(nameof(ArticleEntity.CreatedAt))]
    [MapperIgnoreTarget(nameof(ArticleEntity.UpdatedAt))]
    public static partial ArticleEntity ToEntity(this UpdateArticleCommand command);

    [MapperIgnoreSource(nameof(ArticleEntity.CreatedAt))]
    [MapperIgnoreSource(nameof(ArticleEntity.UpdatedAt))]
    public static partial GetArticleResponse ToDto(this ArticleEntity entity);

    public static partial IQueryable<GetArticleResponse> ProjectToDto(this IQueryable<ArticleEntity> query);

    [MapperIgnoreTarget(nameof(ArticleEntity.Id))]
    [MapperIgnoreSource(nameof(ArticleEntity.Id))]
    public static partial void CopyTo(this ArticleEntity source, ArticleEntity target);
}
