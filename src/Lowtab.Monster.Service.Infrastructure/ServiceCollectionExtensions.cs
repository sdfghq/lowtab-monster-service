using Lowtab.Monster.Service.Application.Interfaces;
using Lowtab.Monster.Service.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Sdf.Platform.EntityFrameworkCore.Interceptors;
using Sdf.Platform.EntityFrameworkCore.Postgresql;
using Sdf.Platform.EntityFrameworkCore.Postgresql.Extensions;
using Sdf.Platform.Redis.Extensions;

namespace Lowtab.Monster.Service.Infrastructure;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        services.ConfigureDatabase(configuration);
        services.ConfigureCaching(configuration);

        return services;
    }

    public static IServiceCollection ConfigureDatabase(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddTransient<AuditableEntitySaveChangesInterceptor>();

        services.AddDbContext<InternalDbContext>((_, options) =>
        {
            options.UseNpgsql(configuration.GetConnectionString(Constants.ConnectionStringName), b =>
                {
                    b.SetPostgresVersion(15, 0);
                    b.MigrationsAssembly(typeof(InternalDbContext).Assembly.FullName);
                    b.EnableRetryOnFailure();
                    // b.MapEnum<GroupTagEnum>("tag_group");
                })
                .UseSnakeCaseNamingConvention()
                .EnableSensitiveDataLogging();
        });

        services.AddScoped<IDbContext, InternalDbContext>();

        services.AddEntityFrameworkCoreHealthChecks<InternalDbContext>();
        services.AddEntityFrameworkCoreOpenTelemetry();

        return services;
    }

    private static IServiceCollection ConfigureCaching(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddPlatformRedis(configuration);
        return services;
    }
}
