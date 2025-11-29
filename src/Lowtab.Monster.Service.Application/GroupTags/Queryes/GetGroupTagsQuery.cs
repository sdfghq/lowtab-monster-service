using Mediator;
using Lowtab.Monster.Service.Contracts.GroupTags.Common;
using Lowtab.Monster.Service.Contracts.GroupTags.GetGroupTags;

namespace Lowtab.Monster.Service.Application.GroupTags.Queryes;

/// <summary>
///     Запрос для получения объекта <see cref="GroupTagBase"/>
/// </summary>
public record GetGroupTagsQuery : GetGroupTagsRequest, IQuery<GetGroupTagsResponse>;
