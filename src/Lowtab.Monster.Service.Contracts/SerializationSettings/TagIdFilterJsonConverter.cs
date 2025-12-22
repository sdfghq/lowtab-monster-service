using System.Text.Json;
using System.Text.Json.Serialization;
using Lowtab.Monster.Service.Contracts.Tags.Common;

namespace Lowtab.Monster.Service.Contracts.SerializationSettings;

/// <summary>
///     JSON converter для <see cref="TagIdFilter" />
/// </summary>
public class TagIdFilterJsonConverter : JsonConverter<TagIdFilter>
{
    /// <inheritdoc />
    public override TagIdFilter Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        var value = reader.GetString();
        return TagIdFilter.Parse(value ?? string.Empty);
    }

    /// <inheritdoc />
    public override void Write(Utf8JsonWriter writer, TagIdFilter value, JsonSerializerOptions options)
    {
        writer.WriteStringValue(value.ToString());
    }
}
