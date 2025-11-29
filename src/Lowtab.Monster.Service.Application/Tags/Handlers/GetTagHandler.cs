using Lowtab.Monster.Service.Application.Common.Exceptions;
using Lowtab.Monster.Service.Application.Interfaces;
using Lowtab.Monster.Service.Application.Tags.Queryes;
using Lowtab.Monster.Service.Contracts.Tags.GetTag;
using Mediator;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using TagMapper = Lowtab.Monster.Service.Application.Tags.Mappings.TagMapper;

namespace Lowtab.Monster.Service.Application.Tags.Handlers;

internal class GetTagHandler(
    ILogger<GetTagHandler> logger,
    IDbContext context
) : IQueryHandler<GetTagQuery, GetTagResponse>
{
    public async ValueTask<GetTagResponse> Handle(GetTagQuery request, CancellationToken ct)
    {
        logger.LogInformation("Try getting {EntityId} from database", request.Id);
        var entity = await context.Tags.FirstOrDefaultAsync(x => x.Id == request.Id, ct) ??
                     throw new NotFoundException($"Не нашел объект с идентификатором {request.Id}");
        var result = TagMapper.ToDto(entity);
        return result;
    }
}
