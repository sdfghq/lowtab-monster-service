using System.Reflection;
using Lowtab.Monster.Service.Cli;
using Lowtab.Monster.Service.Infrastructure;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Sdf.Platform.Observability.Logging;

var builder = Host.CreateDefaultBuilder(args)
    .ConfigureAppConfiguration(x => x.SetBasePath(Path.GetDirectoryName(Assembly.GetEntryAssembly()!.Location)!))
    .ConfigureServices((context, services) =>
    {
        services.AddPlatformLogging(context.Configuration);
        services.ConfigureDatabase(context.Configuration);
        services.AddHostedService<DbMigrationsService>();
    });

using var app = builder.Build();

await app.StartAsync();
