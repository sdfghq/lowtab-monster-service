using Riok.Mapperly.Abstractions;
using Lowtab.Monster.Service.Application.GroupTags.Commands;
using Lowtab.Monster.Service.Contracts.GroupTags.GetGroupTag;
using Lowtab.Monster.Service.Domain.Entities;

namespace Lowtab.Monster.Service.Application.GroupTags.Mappings;

[Mapper(UseDeepCloning = true, EnumMappingStrategy = EnumMappingStrategy.ByName)]
internal static partial class GroupTagMapper
{
    [MapperIgnoreTarget(nameof(GroupTagEntity.Id))]
    [MapperIgnoreTarget(nameof(GroupTagEntity.CreatedAt))]
    [MapperIgnoreTarget(nameof(GroupTagEntity.UpdatedAt))]
    public static partial GroupTagEntity ToEntity(this CreateGroupTagCommand command);

    [MapperIgnoreTarget(nameof(GroupTagEntity.CreatedAt))]
    [MapperIgnoreTarget(nameof(GroupTagEntity.UpdatedAt))]
    public static partial GroupTagEntity ToEntity(this UpdateGroupTagCommand command);

    [MapperIgnoreSource(nameof(GroupTagEntity.CreatedAt))]
    [MapperIgnoreSource(nameof(GroupTagEntity.UpdatedAt))]
    public static partial GetGroupTagResponse ToDto(this GroupTagEntity entity);

    public static partial IQueryable<GetGroupTagResponse> ProjectToDto(this IQueryable<GroupTagEntity> query);

    [MapperIgnoreTarget(nameof(GroupTagEntity.Id))]
    [MapperIgnoreSource(nameof(GroupTagEntity.Id))]
    public static partial void CopyTo(this GroupTagEntity source, GroupTagEntity target);
}
