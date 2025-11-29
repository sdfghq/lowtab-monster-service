using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.Configuration;

namespace Lowtab.Monster.Service.Api.UnitTests;

internal sealed class CustomWebApplicationFactory : WebApplicationFactory<Program>
{
    private const string TestEnvironment = "test";

    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        var testConfigurationBuilder = new ConfigurationBuilder()
            .AddJsonFile($"appsettings.{TestEnvironment}.json")
            .Build();

        builder
            .UseEnvironment(TestEnvironment)
            .UseConfiguration(testConfigurationBuilder);

        //заменяем внешние сервисы локальными, что бы не зависеть от инфраструктуры
        builder.ConfigureServices(services => services.OverrideServicesForTests());
    }
}
