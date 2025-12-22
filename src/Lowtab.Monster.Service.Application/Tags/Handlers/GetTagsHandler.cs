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

        if (request.IdFilter?.Count > 0)
        {
            var predicate = PredicateBuilder.New<TagEntity>();

            foreach (var filter in request.IdFilter)
            {
                var localPredicate = PredicateBuilder.New<TagEntity>();
                if (filter.Id is not null)
                {
                    localPredicate.And(x => x.Id.Contains(filter.Id));
                }

                if (filter.Group is not null)
                {
                    localPredicate.And(x => x.Group == filter.Group);
                }

                predicate.Or(localPredicate);
            }

            query = query.Where(predicate);
        }

        var found = await query.CountAsync(ct);

        var subquery = query.ProjectToDto()
            .Skip(request.Offset)
            .Take(request.Limit);

        var result = await subquery.ToListAsync(ct);

        return new GetTagsResponse { TotalCount = total, TotalFound = found, Items = result };
    }
}
