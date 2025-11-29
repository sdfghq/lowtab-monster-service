using Lowtab.Monster.Service.Application.Tags.Commands;
using Lowtab.Monster.Service.Contracts.Tags.Common;
using Lowtab.Monster.Service.Contracts.Tags.GetTag;
using Lowtab.Monster.Service.Domain.Entities;
using Riok.Mapperly.Abstractions;

namespace Lowtab.Monster.Service.Application.Tags.Mappings;

[Mapper(UseDeepCloning = true, EnumMappingStrategy = EnumMappingStrategy.ByName)]
internal static partial class TagMapper
{
    [MapperIgnoreTarget(nameof(TagEntity.CreatedAt))]
    [MapperIgnoreTarget(nameof(TagEntity.UpdatedAt))]
    public static partial TagEntity ToEntity(this CreateTagCommand command);

    [MapperIgnoreTarget(nameof(TagEntity.CreatedAt))]
    [MapperIgnoreTarget(nameof(TagEntity.UpdatedAt))]
    public static partial TagEntity ToEntity(this UpdateTagCommand command);

    [MapperIgnoreSource(nameof(TagEntity.CreatedAt))]
    [MapperIgnoreSource(nameof(TagEntity.UpdatedAt))]
    public static partial GetTagResponse ToDto(this TagEntity entity);

    public static partial IQueryable<GetTagResponse> ProjectToDto(this IQueryable<TagEntity> query);

    [MapperIgnoreTarget(nameof(TagEntity.Id))]
    [MapperIgnoreSource(nameof(TagEntity.Id))]
    public static partial void CopyTo(this TagEntity source, TagEntity target);

    private static TagId MapTagId(TagId source)
    {
        return source;
    }
}
