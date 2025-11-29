using Lowtab.Monster.Service.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace Lowtab.Monster.Service.Cli;

public sealed class DbMigrationsService(InternalDbContext context, ILogger<DbMigrationsService> logger) : IHostedService
{
    public async Task StartAsync(CancellationToken cancellationToken)
    {
        try
        {
            logger.LogInformation("Starting DB Migration");
            await context.Database.MigrateAsync(cancellationToken);
            logger.LogInformation("DB Migration complete");
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "An error occurred while initialising the database");
            throw;
        }
    }

    public Task StopAsync(CancellationToken cancellationToken)
    {
        return Task.CompletedTask;
    }
}
