using Lowtab.Monster.Service.Application.Articles.Queryes;
using Lowtab.Monster.Service.Application.Common.Exceptions;
using Lowtab.Monster.Service.Application.Interfaces;
using Lowtab.Monster.Service.Contracts.Articles.GetArticle;
using Mediator;
using Microsoft.Extensions.Logging;
using ArticleMapper = Lowtab.Monster.Service.Application.Articles.Mappings.ArticleMapper;

namespace Lowtab.Monster.Service.Application.Articles.Handlers;

internal class GetArticleHandler(
    ILogger<GetArticleHandler> logger,
    IDbContext context
) : IQueryHandler<GetArticleQuery, GetArticleResponse>
{
    public async ValueTask<GetArticleResponse> Handle(GetArticleQuery request, CancellationToken ct)
    {
        logger.LogInformation("Try getting {EntityId} from database", request.Id);
        var entity = await context.Articles.FindAsync([request.Id], ct) ??
                     throw new NotFoundException($"Не нашел объект с идентификатором {request.Id}");

        var result = ArticleMapper.ToDto(entity);
        return result;
    }
}
