using Mediator;
using Microsoft.EntityFrameworkCore;
using Lowtab.Monster.Service.Application.Interfaces;
using Lowtab.Monster.Service.Application.Tags.Mappings;
using Lowtab.Monster.Service.Application.Tags.Queryes;
using Lowtab.Monster.Service.Contracts.Tags.GetTags;

namespace Lowtab.Monster.Service.Application.Tags.Handlers;

internal class GetTagsHandler(IDbContext context) : IQueryHandler<GetTagsQuery, GetTagsResponse>
{
    public async ValueTask<GetTagsResponse> Handle(GetTagsQuery request, CancellationToken ct)
    {
        var query = context.Tags.AsNoTracking().AsQueryable();

        var total = await query.CountAsync(ct);

        if (request.NameFilter is not null)
        {
            query = query.Where(x => x.Id == request.NameFilter);
        }

        var found = await query.CountAsync(ct);

        var result = await query.ProjectToDto()
            .Skip(request.Offset)
            .Take(request.Limit)
            .ToListAsync(ct);

        return new GetTagsResponse
        {
            TotalCount = total,
            TotalFound = found,
            Items = result
        };
    }
}
