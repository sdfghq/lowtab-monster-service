using Lowtab.Monster.Service.Application.Interfaces;
using Lowtab.Monster.Service.Application.Tags.Commands;
using Lowtab.Monster.Service.Application.Tags.Mappings;
using Lowtab.Monster.Service.Contracts.Tags.CreateTag;
using Mediator;
using Microsoft.Extensions.Logging;

namespace Lowtab.Monster.Service.Application.Tags.Handlers;

internal class CreateTagHandler(
    ILogger<CreateTagHandler> logger,
    IDbContext context
) : ICommandHandler<CreateTagCommand, CreateTagResponse>
{
    public async ValueTask<CreateTagResponse> Handle(CreateTagCommand request, CancellationToken ct)
    {
        var newEntity = request.ToEntity();
        logger.LogInformation("Try saving {@Entity} to database", newEntity);

        var result = context.Tags.Add(newEntity).Entity;
        await context.SaveChangesAsync(ct);

        logger.LogInformation("Created new {@Object} with {Id}", result, result.Id);
        return new CreateTagResponse { Id = result.Id, Group = result.Group };
    }
}
