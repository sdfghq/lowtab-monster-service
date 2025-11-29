using FluentValidation;
using Lowtab.Monster.Service.Application.Common.Validators;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Sdf.Platform.Mediator.Extensions;

namespace Lowtab.Monster.Service.Application;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddApplication(this IServiceCollection services, IConfiguration configuration)
    {
        var applicationAssembly = typeof(NotNullRequestValidator<>).Assembly;

        services.AddSingleton(TimeProvider.System);
        services.AddValidatorsFromAssembly(applicationAssembly, includeInternalTypes: true);
        services.AddMediator(options => options.ServiceLifetime = ServiceLifetime.Scoped);
        services.AddPlatformBehaviors(applicationAssembly);

        return services;
    }
}
