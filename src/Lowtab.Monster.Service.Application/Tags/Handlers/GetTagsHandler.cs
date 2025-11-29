using LinqKit;
using Lowtab.Monster.Service.Application.Interfaces;
using Lowtab.Monster.Service.Application.Tags.Mappings;
using Lowtab.Monster.Service.Application.Tags.Queryes;
using Lowtab.Monster.Service.Contracts.Tags.GetTags;
using Lowtab.Monster.Service.Domain.Entities;
using Mediator;
using Microsoft.EntityFrameworkCore;

namespace Lowtab.Monster.Service.Application.Tags.Handlers;

internal class GetTagsHandler(IDbContext context) : IQueryHandler<GetTagsQuery, GetTagsResponse>
{
    public async ValueTask<GetTagsResponse> Handle(GetTagsQuery request, CancellationToken ct)
    {
        var query = context.Tags.AsNoTracking().AsQueryable();

        var total = await query.CountAsync(ct);

        if (request.IdFilter is not null)
        {
            query = query.Where(x => x.Id == request.IdFilter);
        }

        if (request.GroupsFilter?.Count > 0)
        {
            var predicate = PredicateBuilder.New<TagEntity>();
            foreach (var group in request.GroupsFilter)
            {
                predicate = predicate.Or(x => x.Group == group);
            }

            query = query.Where(predicate);
        }

        var found = await query.CountAsync(ct);

        var result = await query.ProjectToDto()
            .Skip(request.Offset)
            .Take(request.Limit)
            .ToListAsync(ct);

        return new GetTagsResponse { TotalCount = total, TotalFound = found, Items = result };
    }
}
