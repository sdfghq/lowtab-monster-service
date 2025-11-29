using System.Text.Json;
using System.Text.Json.Serialization;
using Lowtab.Monster.Service.Contracts.Tags.Common;

namespace Lowtab.Monster.Service.Contracts.SerializationSettings;

/// <inheritdoc />
public class TagIdJsonConverter : JsonConverter<TagId>
{
    /// <inheritdoc />
    public override TagId Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        if (reader.TokenType == JsonTokenType.String)
        {
            var value = reader.GetString();
            return TagId.Parse(value!);
        }

        if (reader.TokenType == JsonTokenType.StartObject)
        {
            GroupTagEnum group = default;
            string? id = null;

            while (reader.Read())
            {
                if (reader.TokenType == JsonTokenType.EndObject)
                {
                    break;
                }

                if (reader.TokenType == JsonTokenType.PropertyName)
                {
                    var propertyName = reader.GetString();
                    reader.Read();

                    if (string.Equals(propertyName, "group", StringComparison.OrdinalIgnoreCase))
                    {
                        group = JsonSerializer.Deserialize<GroupTagEnum>(ref reader, options);
                    }
                    else if (string.Equals(propertyName, "id", StringComparison.OrdinalIgnoreCase))
                    {
                        id = reader.GetString();
                    }
                    else
                    {
                        reader.Skip();
                    }
                }
            }

            return new TagId(group, id!);
        }

        throw new JsonException($"Unexpected token type {reader.TokenType}");
    }

    /// <inheritdoc />
    public override void Write(Utf8JsonWriter writer, TagId value, JsonSerializerOptions options)
    {
        ArgumentNullException.ThrowIfNull(writer);
        writer.WriteStringValue(value.ToString());
    }
}
