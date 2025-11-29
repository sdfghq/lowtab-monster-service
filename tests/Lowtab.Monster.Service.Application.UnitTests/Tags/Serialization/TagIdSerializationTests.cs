using System.Text.Json;
using FluentAssertions;
using Lowtab.Monster.Service.Contracts.SerializationSettings;
using Lowtab.Monster.Service.Contracts.Tags.Common;
using Xunit;

namespace Lowtab.Monster.Service.Application.UnitTests.Tags.Serialization;

public class TagIdSerializationTests
{
    private readonly JsonSerializerOptions _options;

    public TagIdSerializationTests()
    {
        _options = new JsonSerializerOptions();
        _options.Converters.Add(new TagIdJsonConverter());
        _options.PropertyNameCaseInsensitive = true;
    }

    [Fact]
    public void Deserialize_FromString_ReturnsTagId()
    {
        // Arrange
        var json = "\"Map:Dust2\"";

        // Act
        var result = JsonSerializer.Deserialize<TagId>(json, _options);

        // Assert
        result.Group.Should().Be(GroupTagEnum.Map);
        result.Id.Should().Be("Dust2");
    }

    [Fact]
    public void Deserialize_FromObject_ReturnsTagId()
    {
        // Arrange
        var json = """
                   {
                       "group": "Map",
                       "id": "Dust2"
                   }
                   """;

        // Act
        var result = JsonSerializer.Deserialize<TagId>(json, _options);

        // Assert
        result.Group.Should().Be(GroupTagEnum.Map);
        result.Id.Should().Be("Dust2");
    }

    [Fact]
    public void Deserialize_FromObject_CaseInsensitive_ReturnsTagId()
    {
        // Arrange
        var json = """
                   {
                       "Group": "Map",
                       "Id": "Dust2"
                   }
                   """;

        // Act
        var result = JsonSerializer.Deserialize<TagId>(json, _options);

        // Assert
        result.Group.Should().Be(GroupTagEnum.Map);
        result.Id.Should().Be("Dust2");
    }

    [Fact]
    public void Deserialize_FromObject_WithExtraProperties_ReturnsTagId()
    {
        // Arrange
        var json = """
                   {
                       "group": "Map",
                       "extra": "value",
                       "id": "Dust2"
                   }
                   """;

        // Act
        var result = JsonSerializer.Deserialize<TagId>(json, _options);

        // Assert
        result.Group.Should().Be(GroupTagEnum.Map);
        result.Id.Should().Be("Dust2");
    }

    [Fact]
    public void Serialize_ReturnsString()
    {
        // Arrange
        var tagId = new TagId(GroupTagEnum.Map, "Dust2");

        // Act
        var json = JsonSerializer.Serialize(tagId, _options);

        // Assert
        json.Should().Be("\"Map:Dust2\"");
    }
}
