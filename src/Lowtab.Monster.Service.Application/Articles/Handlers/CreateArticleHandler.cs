using Lowtab.Monster.Service.Application.Articles.Commands;
using Lowtab.Monster.Service.Application.Articles.Mappings;
using Lowtab.Monster.Service.Application.Interfaces;
using Lowtab.Monster.Service.Contracts.Articles.CreateArticle;
using Mediator;
using Microsoft.Extensions.Logging;

namespace Lowtab.Monster.Service.Application.Articles.Handlers;

internal class CreateArticleHandler(
    ILogger<CreateArticleHandler> logger,
    IDbContext context
) : ICommandHandler<CreateArticleCommand, CreateArticleResponse>
{
    public async ValueTask<CreateArticleResponse> Handle(CreateArticleCommand request, CancellationToken ct)
    {
        var newEntity = request.ToEntity();
        logger.LogInformation("Try saving {@Entity} to database", newEntity);

        var result = context.Articles.Add(newEntity).Entity;
        await context.SaveChangesAsync(ct);

        logger.LogInformation("Created new {@Object} with {Id}", result, result.Id);
        return new CreateArticleResponse { Id = result.Id };
    }
}
