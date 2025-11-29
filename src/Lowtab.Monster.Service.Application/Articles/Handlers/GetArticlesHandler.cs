using LinqKit;
using Lowtab.Monster.Service.Application.Articles.Mappings;
using Lowtab.Monster.Service.Application.Articles.Queryes;
using Lowtab.Monster.Service.Application.Interfaces;
using Lowtab.Monster.Service.Contracts.Articles.GetArticles;
using Lowtab.Monster.Service.Domain.Entities;
using Mediator;
using Microsoft.EntityFrameworkCore;

namespace Lowtab.Monster.Service.Application.Articles.Handlers;

internal class GetArticlesHandler(IDbContext context) : IQueryHandler<GetArticlesQuery, GetArticlesResponse>
{
    public async ValueTask<GetArticlesResponse> Handle(GetArticlesQuery request, CancellationToken ct)
    {
        var query = context.Articles
            .Include(x => x.Tags)
            .AsNoTracking()
            .AsQueryable();

        var total = await query.CountAsync(ct);

        if (request.TextFilter is not null)
        {
            query = query.Where(x => x.Title.ToLower().Contains(request.TextFilter.ToLower()));
            query = query.Where(x => x.Body.ToLower().Contains(request.TextFilter.ToLower()));
        }

        if (request.TagFilter?.Count > 0)
        {
            var predicate = PredicateBuilder.New<ArticleEntity>();

            foreach (var tagFilter in request.TagFilter)
            {
                var tagPredicate = PredicateBuilder.New<TagEntity>();
                if (!string.IsNullOrEmpty(tagFilter.Tag))
                {
                    tagPredicate.And(x => x.Id.Equals(tagFilter.Tag));
                }

                if (tagFilter.Group.HasValue)
                {
                    tagPredicate.And(x => x.Group == tagFilter.Group.Value);
                }

                predicate.And(x => x.Tags!.Any(tagPredicate));
            }

            query = query.Where(predicate);
        }

        var found = await query.CountAsync(ct);

        var result = await query.ProjectToDto()
            .Skip(request.Offset)
            .Take(request.Limit)
            .ToListAsync(ct);

        return new GetArticlesResponse { TotalCount = total, TotalFound = found, Items = result };
    }
}
