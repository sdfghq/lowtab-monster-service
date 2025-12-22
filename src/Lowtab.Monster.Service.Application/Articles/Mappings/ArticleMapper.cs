using Lowtab.Monster.Service.Application.Articles.Commands;
using Lowtab.Monster.Service.Contracts.Articles.GetArticle;
using Lowtab.Monster.Service.Contracts.Tags.Common;
using Lowtab.Monster.Service.Domain.Entities;
using Riok.Mapperly.Abstractions;

namespace Lowtab.Monster.Service.Application.Articles.Mappings;

[Mapper(UseDeepCloning = true, EnumMappingStrategy = EnumMappingStrategy.ByName)]
internal static partial class ArticleMapper
{
    [MapperIgnoreTarget(nameof(ArticleEntity.Id))]
    [MapperIgnoreTarget(nameof(ArticleEntity.CreatedAt))]
    [MapperIgnoreTarget(nameof(ArticleEntity.UpdatedAt))]
    [MapperIgnoreTarget(nameof(ArticleEntity.Tags))]
    public static partial ArticleEntity ToEntity(this CreateArticleCommand command);

    [MapperIgnoreTarget(nameof(ArticleEntity.CreatedAt))]
    [MapperIgnoreTarget(nameof(ArticleEntity.UpdatedAt))]
    [MapperIgnoreTarget(nameof(ArticleEntity.Tags))]
    public static partial ArticleEntity ToEntity(this UpdateArticleCommand command);

    [MapperIgnoreSource(nameof(ArticleEntity.CreatedAt))]
    [MapperIgnoreSource(nameof(ArticleEntity.UpdatedAt))]
    public static partial GetArticleResponse ToDto(this ArticleEntity entity);

    public static partial IQueryable<GetArticleResponse> ProjectToDto(this IQueryable<ArticleEntity> query);

    [MapperIgnoreTarget(nameof(ArticleEntity.Id))]
    [MapperIgnoreSource(nameof(ArticleEntity.Id))]
    [MapperIgnoreSource(nameof(ArticleEntity.Tags))]
    [MapperIgnoreTarget(nameof(ArticleEntity.Tags))]
    public static partial void CopyTo(this ArticleEntity source, ArticleEntity target);

    [MapProperty(nameof(entity), nameof(Tag.Id), Use = nameof(MapToTagId))]
    private static partial Tag ToTagDto(TagEntity entity);

    [UserMapping]
    private static TagId MapToTagId(TagEntity source)
    {
        return new TagId(source.Group, source.Id);
    }
}
