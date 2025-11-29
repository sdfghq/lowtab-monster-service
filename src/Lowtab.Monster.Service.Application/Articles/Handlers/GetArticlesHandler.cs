using Mediator;
using Microsoft.EntityFrameworkCore;
using Lowtab.Monster.Service.Application.Interfaces;
using Lowtab.Monster.Service.Application.Articles.Mappings;
using Lowtab.Monster.Service.Application.Articles.Queryes;
using Lowtab.Monster.Service.Contracts.Articles.GetArticles;

namespace Lowtab.Monster.Service.Application.Articles.Handlers;

internal class GetArticlesHandler(IDbContext context) : IQueryHandler<GetArticlesQuery, GetArticlesResponse>
{
    public async ValueTask<GetArticlesResponse> Handle(GetArticlesQuery request, CancellationToken ct)
    {
        var query = context.Articles.AsNoTracking().AsQueryable();

        var total = await query.CountAsync(ct);

        if (request.NameFilter is not null)
        {
            query = query.Where(x => x.Title == request.NameFilter);
        }

        var found = await query.CountAsync(ct);

        var result = await query.ProjectToDto()
            .Skip(request.Offset)
            .Take(request.Limit)
            .ToListAsync(ct);

        return new GetArticlesResponse
        {
            TotalCount = total,
            TotalFound = found,
            Items = result
        };
    }
}
