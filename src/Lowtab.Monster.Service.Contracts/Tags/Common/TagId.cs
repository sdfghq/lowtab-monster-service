using System.ComponentModel;
using System.Text.Json.Serialization;
using Lowtab.Monster.Service.Contracts.SerializationSettings;

namespace Lowtab.Monster.Service.Contracts.Tags.Common;

/// <summary>
///     Композитный идентификатор тега
/// </summary>
[TypeConverter(typeof(TagIdTypeConverter))]
[JsonConverter(typeof(TagIdJsonConverter))]
public readonly record struct TagId
{
    private const char Separator = ':';

    /// <summary>
    ///     Конструктор композитного идентификатора тега
    /// </summary>
    /// <param name="group"></param>
    /// <param name="id"></param>
    /// <exception cref="ArgumentException"></exception>
    public TagId(GroupTag group, string id)
    {
        if (string.IsNullOrWhiteSpace(id))
        {
            throw new ArgumentException("Id cannot be empty", nameof(id));
        }

        if (group == 0)
        {
            throw new ArgumentException("Group cannot be empty", nameof(group));
        }

        Group = group;
        Id = id;
    }

    /// <summary>
    ///     Группа тега
    /// </summary>
    public GroupTag Group { get; }

    /// <summary>
    ///     Идентификатор тега
    /// </summary>
    public string Id { get; }

    /// <inheritdoc />
    public override string ToString()
    {
        return $"{Group}{Separator}{Id}";
    }

    /// <summary>
    ///     Парсинг композитного идентификатора тега из строки
    /// </summary>
    /// <param name="value"></param>
    /// <returns></returns>
    /// <exception cref="FormatException"></exception>
    public static TagId Parse(string value)
    {
        return TryParse(value, out var result) ? result : throw new FormatException($"Invalid TagId format: {value}");
    }

    /// <summary>
    ///     Попытка парсинга композитного идентификатора тега из строки
    /// </summary>
    /// <param name="value"></param>
    /// <param name="result"></param>
    /// <returns></returns>
    public static bool TryParse(string? value, out TagId result)
    {
        result = default;

        if (string.IsNullOrWhiteSpace(value))
        {
            return false;
        }

        var parts = value.Split(Separator);
        if (parts.Length != 2)
        {
            return false;
        }

        if (!Enum.TryParse<GroupTag>(parts[0], true, out var group))
        {
            return false;
        }

        if (string.IsNullOrWhiteSpace(parts[1]))
        {
            return false;
        }

        result = new TagId(group, parts[1]);
        return true;
    }
}
