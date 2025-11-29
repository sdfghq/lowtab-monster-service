using System.Reflection;
using Lowtab.Monster.Service.Api;
using Lowtab.Monster.Service.Api.Endpoints;
using Lowtab.Monster.Service.Application;
using Lowtab.Monster.Service.Infrastructure;
using Sdf.Platform.Versioning.Swagger;
using Sdf.Platform.Web.Sdk;

var entryAssembly = Assembly.GetEntryAssembly()!;
var builder = WebApplication.CreateBuilder(new WebApplicationOptions { Args = args });
builder.AddPlatformWebDefaults(new PlatformApiDefinition(entryAssembly.GetName().Name!, "1.0.0"));

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

namespace Lowtab.Monster.Service.Api
{
    public partial class Program;
}
