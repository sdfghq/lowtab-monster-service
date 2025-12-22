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
    [MapProperty(nameof(CreateTagCommand.Id.Id), nameof(TagEntity.Id))]
    [MapProperty(nameof(CreateTagCommand.Id.Group), nameof(TagEntity.Group))]
    public static partial TagEntity ToEntity(this CreateTagCommand command);

    [MapperIgnoreTarget(nameof(TagEntity.CreatedAt))]
    [MapperIgnoreTarget(nameof(TagEntity.UpdatedAt))]
    [MapProperty(nameof(CreateTagCommand.Id.Id), nameof(TagEntity.Id))]
    [MapProperty(nameof(CreateTagCommand.Id.Group), nameof(TagEntity.Group))]
    public static partial TagEntity ToEntity(this UpdateTagCommand command);

    [MapperIgnoreSource(nameof(TagEntity.CreatedAt))]
    [MapperIgnoreSource(nameof(TagEntity.UpdatedAt))]
    [MapProperty(nameof(entity), nameof(GetTagResponse.Id), Use = nameof(MapTagId))]
    public static partial GetTagResponse ToDto(this TagEntity entity);

    public static partial IQueryable<GetTagResponse> ProjectToDto(this IQueryable<TagEntity> query);

    [MapperIgnoreTarget(nameof(TagEntity.Id))]
    [MapperIgnoreSource(nameof(TagEntity.Id))]
    public static partial void CopyTo(this TagEntity source, TagEntity target);

    [UserMapping]
    private static TagId MapTagId(TagEntity source)
    {
        return new TagId(source.Group, source.Id);
    }
}
