using FluentValidation;
using Lowtab.Monster.Service.Application.Common.Exceptions;
using Lowtab.Monster.Service.Contracts.SerializationSettings;
using Microsoft.AspNetCore.Mvc;
using Sdf.Platform.ExceptionHandling;

namespace Lowtab.Monster.Service.Api;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddWeb(this IServiceCollection services, IConfiguration _)
    {
        services.AddHttpContextAccessor();
        services.Configure<ApiBehaviorOptions>(options => options.SuppressModelStateInvalidFilter = true);

        services.ConfigureHttpJsonOptions(options => options.SerializerOptions.ConfigureJsonSerializerOptions());
        services.Configure<JsonOptions>(options => options.JsonSerializerOptions.ConfigureJsonSerializerOptions());

        services.AddEndpointsApiExplorer();
        services.ConfigureExceptionHandler();

        return services;
    }

    private static void ConfigureExceptionHandler(this IServiceCollection services)
    {
        services.Configure<PlatformExceptionHandlerOptions>(options =>
        {
            var oldStatusCodeDelegate = options.StatusCodeDelegate;
            options.StatusCodeDelegate = exception => exception switch
            {
                NotFoundException => (StatusCodes.Status404NotFound, "Not found"),
                ValidationException => (StatusCodes.Status400BadRequest, "Bad request"),
                _ => oldStatusCodeDelegate.Invoke(exception)
            };
        });
    }
}
