using System.Text.Encodings.Web;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Lowtab.Monster.Service.Contracts.SerializationSettings;

/// <inheritdoc cref="SerializerSettings.LowtabMonsterServiceOptions" />
public static class SerializerSettings
{
    /// <summary>
    ///     Настройки сериализации для API Lowtab.Monster.Service
    /// </summary>
    public static JsonSerializerOptions LowtabMonsterServiceOptions { get; } =
        ConfigureJsonSerializerOptions(new JsonSerializerOptions());

    /// <summary>
    ///     Конфигурация <see cref="JsonSerializerOptions" />
    /// </summary>
    /// <param name="serializerOptions"></param>
    /// <returns></returns>
    public static JsonSerializerOptions ConfigureJsonSerializerOptions(this JsonSerializerOptions serializerOptions)
    {
        ArgumentNullException.ThrowIfNull(serializerOptions);
        serializerOptions.PropertyNamingPolicy = JsonNamingPolicy.CamelCase;
        serializerOptions.Converters.Add(new JsonStringEnumConverter());
        serializerOptions.Converters.Add(new TagIdJsonConverter());
        serializerOptions.DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull;
        serializerOptions.Encoder = JavaScriptEncoder.UnsafeRelaxedJsonEscaping;
        return serializerOptions;
    }
}
