using Lowtab.Monster.Service.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Sdf.Platform.EntityFrameworkCore.Interceptors;

namespace Lowtab.Monster.Service.Application.UnitTests;

public sealed class InternalDbContextFactory : IDesignTimeDbContextFactory<InternalDbContext>
{
    public InternalDbContext CreateDbContext(string[] args)
    {
        return CreateInMemoryContext(args.FirstOrDefault());
    }

    private static InternalDbContext CreateInMemoryContext(string? databaseName = default)
    {
        var optionsBuilder = new DbContextOptionsBuilder<InternalDbContext>()
            .UseInMemoryDatabase(databaseName ?? nameof(InternalDbContext))
            .ConfigureWarnings(builder => builder.Ignore(InMemoryEventId.TransactionIgnoredWarning));

        return new InternalDbContext(optionsBuilder.Options,
            new AuditableEntitySaveChangesInterceptor(TimeProvider.System));
    }
}
