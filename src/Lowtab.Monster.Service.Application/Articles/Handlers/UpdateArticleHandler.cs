using Mediator;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Lowtab.Monster.Service.Application.Common.Exceptions;
using Lowtab.Monster.Service.Application.Interfaces;
using Lowtab.Monster.Service.Application.Articles.Commands;
using Lowtab.Monster.Service.Application.Articles.Mappings;
using Lowtab.Monster.Service.Contracts.Articles.UpdateArticle;

namespace Lowtab.Monster.Service.Application.Articles.Handlers;

internal class UpdateArticleHandler
(
    ILogger<UpdateArticleHandler> logger,
    IDbContext context
) : ICommandHandler<UpdateArticleCommand, UpdateArticleResponse>
{
    public async ValueTask<UpdateArticleResponse> Handle(UpdateArticleCommand request, CancellationToken ct)
    {
        var newEntity = request.ToEntity();

        logger.LogInformation("Try update {@Entity} in database", newEntity);
        var updatedEntity = await context.Articles.FirstOrDefaultAsync(x => x.Id == newEntity.Id, ct) ??
                            throw new NotFoundException($"Не нашел объект с идентификатором {newEntity.Id}");

        newEntity.CopyTo(updatedEntity);

        await context.SaveChangesAsync(ct);
        return new UpdateArticleResponse();
    }
}
