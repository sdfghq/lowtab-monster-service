using Lowtab.Monster.Service.Contracts.Common;
using Lowtab.Monster.Service.Contracts.GroupTags.Common;
using Lowtab.Monster.Service.Contracts.GroupTags.GetGroupTag;

namespace Lowtab.Monster.Service.Contracts.GroupTags.GetGroupTags;

/// <summary>
///     Ответ на запрос получения списка объектов <see cref="GroupTag"/>
/// </summary>
public record GetGroupTagsResponse : PaginatedResponse<GetGroupTagResponse>;
