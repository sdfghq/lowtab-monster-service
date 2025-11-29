using Mediator;
using Microsoft.Extensions.Logging;
using Lowtab.Monster.Service.Application.Common.Exceptions;
using Lowtab.Monster.Service.Application.Interfaces;
using Lowtab.Monster.Service.Application.GroupTags.Queryes;
using Lowtab.Monster.Service.Contracts.GroupTags.GetGroupTag;
using GroupTagMapper = Lowtab.Monster.Service.Application.GroupTags.Mappings.GroupTagMapper;

namespace Lowtab.Monster.Service.Application.GroupTags.Handlers;

internal class GetGroupTagHandler
(
    ILogger<GetGroupTagHandler> logger,
    IDbContext context
) : IQueryHandler<GetGroupTagQuery, GetGroupTagResponse>
{
    public async ValueTask<GetGroupTagResponse> Handle(GetGroupTagQuery request, CancellationToken ct)
    {
        logger.LogInformation("Try getting {EntityId} from database", request.Id);
        var entity = await context.GroupTags.FindAsync([request.Id], ct) ??
                     throw new NotFoundException($"Не нашел объект с идентификатором {request.Id}");

        var result = GroupTagMapper.ToDto(entity);
        return result;
    }
}
