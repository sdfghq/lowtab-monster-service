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
        // Получаем общее количество статей до фильтрации
        var total = await context.Articles.CountAsync(ct);

        var query = context.Articles
            .AsNoTracking()
            .AsQueryable();

        // Фильтрация по тексту (OR между заголовком и телом)
        if (request.TextFilter is not null)
        {
            var searchText = request.TextFilter;
            query = query.Where(x =>
                EF.Functions.Like(x.Title, $"%{searchText}%") ||
                EF.Functions.Like(x.Body, $"%{searchText}%"));
        }

        // Фильтрация по тегам
        if (request.TagFilter?.Count > 0)
        {
            var predicate = PredicateBuilder.New<TagEntity>();

            foreach (var filter in request.TagFilter)
            {
                var localPredicate = PredicateBuilder.New<TagEntity>(true);
                if (filter.Id is not null)
                {
                    localPredicate = localPredicate.And(x => x.Id.Contains(filter.Id));
                }

                if (filter.Group is not null)
                {
                    localPredicate = localPredicate.And(x => x.Group == filter.Group);
                }

                predicate = predicate.Or(localPredicate);
            }

            var tags = context.Tags
                .AsNoTracking()
                .AsExpandableEFCore()
                .Where(predicate);

            query = from article in query
                from tag in tags
                where article.Tags.Contains(tag)
                select article;
        }

        // Подсчет отфильтрованных результатов
        var found = await query.CountAsync(ct);

        // Получение результатов с пагинацией
        // Include добавляем только для финальной выборки
        var result = await query
            .Include(x => x.Tags)
            .Skip(request.Offset)
            .Take(request.Limit)
            .ProjectToDto()
            .ToListAsync(ct);

        return new GetArticlesResponse { TotalCount = total, TotalFound = found, Items = result };
    }
}
