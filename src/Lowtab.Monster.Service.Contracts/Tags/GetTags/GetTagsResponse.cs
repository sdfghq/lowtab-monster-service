using Lowtab.Monster.Service.Contracts.Common;
using Lowtab.Monster.Service.Contracts.Tags.Common;
using Lowtab.Monster.Service.Contracts.Tags.GetTag;

namespace Lowtab.Monster.Service.Contracts.Tags.GetTags;

/// <summary>
///     Ответ на запрос получения списка объектов <see cref="Tag"/>
/// </summary>
public record GetTagsResponse : PaginatedResponse<GetTagResponse>;
