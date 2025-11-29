using Lowtab.Monster.Service.Infrastructure.Persistence;
using Microsoft.Extensions.DependencyInjection;
using Moq;
using StackExchange.Redis;
using InternalDbContextFactory = Lowtab.Monster.Service.Application.UnitTests.InternalDbContextFactory;

namespace Lowtab.Monster.Service.Api.UnitTests;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection OverrideServicesForTests(this IServiceCollection services)
    {
        services
            .AddLogging()
            .AddInMemoryDatabase()
            .AddInMemoryCache()
            ;

        return services;
    }

    private static IServiceCollection Remove<TService>(this IServiceCollection services)
    {
        var serviceDescriptor = services.FirstOrDefault(d => d.ServiceType == typeof(TService));

        if (serviceDescriptor != null)
        {
            services.Remove(serviceDescriptor);
        }

        return services;
    }

    private static IServiceCollection AddInMemoryDatabase(this IServiceCollection services)
    {
        InternalDbContextFactory factory = new();
        services.AddScoped<InternalDbContext>(_ => factory.CreateDbContext([]));

        return services;
    }

    private static IServiceCollection AddInMemoryCache(this IServiceCollection services)
    {
        services.AddSingleton<IConnectionMultiplexer>(_ => new Mock<IConnectionMultiplexer>().Object);
        services.AddDistributedMemoryCache();
        return services;
    }
}
