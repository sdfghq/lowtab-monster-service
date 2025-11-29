using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace Lowtab.Monster.Service.Infrastructure.Persistence;

internal class InternalDbContextFactory : IDesignTimeDbContextFactory<InternalDbContext>
{
    public virtual InternalDbContext CreateDbContext(string[] args)
    {
        return CreateNpgsqlDbContext();
    }

    private static InternalDbContext CreateNpgsqlDbContext()
    {
        var optionsBuilder = new DbContextOptionsBuilder<InternalDbContext>()
            .UseNpgsql("Server=stub;Database=stub;", b =>
            {
                b.SetPostgresVersion(15, 0);
                b.MigrationsAssembly(typeof(InternalDbContext).Assembly.FullName);
            })
            .UseSnakeCaseNamingConvention()
            .EnableSensitiveDataLogging();

        return new InternalDbContext(optionsBuilder.Options, null);
    }
}
