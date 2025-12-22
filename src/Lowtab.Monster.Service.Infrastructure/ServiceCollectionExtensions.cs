using Lowtab.Monster.Service.Application.Interfaces;
using Lowtab.Monster.Service.Contracts.Tags.Common;
using Lowtab.Monster.Service.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Npgsql;
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
            var dataSourceBuilder =
                new NpgsqlDataSourceBuilder(configuration.GetConnectionString(Constants.ConnectionStringName));
            dataSourceBuilder.MapEnum<GroupTag>();
            dataSourceBuilder.MapComposite<TagId>();

            options.UseNpgsql(dataSourceBuilder.Build(), b =>
                {
                    b.SetPostgresVersion(15, 0);
                    b.MigrationsAssembly(typeof(InternalDbContext).Assembly.FullName);
                    b.EnableRetryOnFailure();
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
