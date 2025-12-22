using System.Reflection;
using Lowtab.Monster.Service.Api;
using Lowtab.Monster.Service.Api.Endpoints;
using Lowtab.Monster.Service.Application;
using Lowtab.Monster.Service.Contracts.Tags.Common;
using Lowtab.Monster.Service.Infrastructure;
using NJsonSchema;
using NJsonSchema.Generation.TypeMappers;
using Sdf.Platform.Versioning.Swagger;
using Sdf.Platform.Web.Sdk;

var entryAssembly = Assembly.GetEntryAssembly()!;
var builder = WebApplication.CreateBuilder(new WebApplicationOptions { Args = args });
builder.AddPlatformWebDefaults(
    new PlatformApiDefinition(entryAssembly.GetName().Name!, "1.0.0"),
    settings =>
    {
        settings.SchemaSettings.TypeMappers.Add(new PrimitiveTypeMapper(typeof(TagId),
            schema => { schema.Type = JsonObjectType.String; }));
        settings.SchemaSettings.TypeMappers.Add(new PrimitiveTypeMapper(typeof(TagIdFilter),
            schema => { schema.Type = JsonObjectType.String; }));
    });

builder.Services
    .AddApplication(builder.Configuration)
    .AddInfrastructure(builder.Configuration)
    .AddWeb(builder.Configuration);

var app = builder.Build();

app.UseRouting();
app.UsePlatformWebDefaults();
app.UseStaticFiles();
app.MapTagEndpoints();
app.MapArticleEndpoints();

await app.RunAsync();

public partial class Program;
