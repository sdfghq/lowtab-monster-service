using Mediator;
using Microsoft.EntityFrameworkCore;
using Lowtab.Monster.Service.Application.Interfaces;
using Lowtab.Monster.Service.Application.GroupTags.Mappings;
using Lowtab.Monster.Service.Application.GroupTags.Queryes;
using Lowtab.Monster.Service.Contracts.GroupTags.GetGroupTags;

namespace Lowtab.Monster.Service.Application.GroupTags.Handlers;

internal class GetGroupTagsHandler(IDbContext context) : IQueryHandler<GetGroupTagsQuery, GetGroupTagsResponse>
{
    public async ValueTask<GetGroupTagsResponse> Handle(GetGroupTagsQuery request, CancellationToken ct)
    {
        var query = context.GroupTags.AsNoTracking().AsQueryable();

        var total = await query.CountAsync(ct);

        if (request.NameFilter is not null)
        {
            query = query.Where(x => x.Description == request.NameFilter);
        }

        var found = await query.CountAsync(ct);

        var result = await query.ProjectToDto()
            .Skip(request.Offset)
            .Take(request.Limit)
            .ToListAsync(ct);

        return new GetGroupTagsResponse
        {
            TotalCount = total,
            TotalFound = found,
            Items = result
        };
    }
}
