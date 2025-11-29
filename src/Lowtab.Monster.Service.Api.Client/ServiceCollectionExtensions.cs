// using Microsoft.Extensions.Configuration;
// using Microsoft.Extensions.DependencyInjection;
// using Sdf.Platform.Client.Sdk;
// using Lowtab.Monster.Service.Api.Client.Options;
//
// namespace Lowtab.Monster.Service.Api.Client;
//
// /// <summary>
// ///     Provides extension methods for the service collection.
// /// </summary>
// public static class ServiceCollectionExtensions
// {
//     /// <summary>
//     ///     Добавляет в коллекцию сервисов клиента <see cref="ILowtabMonsterServiceClient" />>
//     /// </summary>
//     /// <param name="services"></param>
//     /// <param name="configuration"></param>
//     /// <returns></returns>
//     public static IHttpClientBuilder ConfigureLowtabMonsterServiceClient(this IServiceCollection services,
//         IConfigurationSection configuration)
//     {
//         ArgumentNullException.ThrowIfNull(configuration);
//
//         var builder = services.AddPlatformHttpClient<ILowtabMonsterServiceClient, LowtabMonsterServiceClient,
//             LowtabMonsterServiceClientOptions>(configuration);
//
//         return builder;
//     }
// }



