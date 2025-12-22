using System.ComponentModel;
using System.Text.Json.Serialization;
using Lowtab.Monster.Service.Contracts.SerializationSettings;

namespace Lowtab.Monster.Service.Contracts.Tags.Common;

/// <summary>
///     Фильтр для поиска по композитному идентификатору тега.
///     Поддерживает частичный поиск по группе и/или идентификатору.
/// </summary>
[TypeConverter(typeof(TagIdFilterTypeConverter))]
[JsonConverter(typeof(TagIdFilterJsonConverter))]
public readonly record struct TagIdFilter
{
    private const char Separator = ':';

    /// <summary>
    ///     Конструктор фильтра для поиска по композитному идентификатору тега
    /// </summary>
    /// <param name="group">Группа тега (может быть null)</param>
    /// <param name="id">Идентификатор тега (может быть null)</param>
    /// <exception cref="ArgumentException">Если оба параметра null или пустые</exception>
    public TagIdFilter(GroupTag? group, string? id)
    {
        if (group is null && string.IsNullOrWhiteSpace(id))
        {
            throw new ArgumentException("At least one of Group or Id must be specified");
        }

        Group = group;
        Id = string.IsNullOrWhiteSpace(id) ? null : id;
    }

    /// <summary>
    ///     Группа тега (опционально)
    /// </summary>
    public GroupTag? Group { get; }

    /// <summary>
    ///     Идентификатор тега (опционально)
    /// </summary>
    public string? Id { get; }

    /// <summary>
    ///     Проверяет, задана ли только группа
    /// </summary>
    public bool IsGroupOnly => Group.HasValue && Id is null;

    /// <summary>
    ///     Проверяет, задан ли только идентификатор
    /// </summary>
    public bool IsIdOnly => Group is null && Id is not null;

    /// <summary>
    ///     Проверяет, заданы ли и группа, и идентификатор
    /// </summary>
    public bool IsComplete => Group.HasValue && Id is not null;

    /// <inheritdoc />
    public override string ToString()
    {
        if (IsGroupOnly)
        {
            return $"{Group}{Separator}";
        }

        return IsIdOnly ? $"{Separator}{Id}" : $"{Group}{Separator}{Id}";
    }

    /// <summary>
    ///     Парсинг фильтра из строки
    /// </summary>
    /// <param name="value">Строка в формате "Group:", ":id", или "Group:id"</param>
    /// <returns>Распарсенный фильтр</returns>
    /// <exception cref="FormatException">Если формат строки неверный</exception>
    public static TagIdFilter Parse(string value)
    {
        return TryParse(value, out var result)
            ? result
            : throw new FormatException($"Invalid TagIdFilter format: {value}");
    }

    /// <summary>
    ///     Попытка парсинга фильтра из строки
    /// </summary>
    /// <param name="value">Строка в формате "Group:", ":id", или "Group:id"</param>
    /// <param name="result">Результат парсинга</param>
    /// <returns>true, если парсинг успешен</returns>
    public static bool TryParse(string? value, out TagIdFilter result)
    {
        result = default;

        if (string.IsNullOrWhiteSpace(value))
        {
            return false;
        }

        var separatorIndex = value.IndexOf(Separator);

        // Нет разделителя - невалидный формат
        if (separatorIndex == -1)
        {
            return false;
        }

        var groupPart = value[..separatorIndex];
        var idPart = value[(separatorIndex + 1)..];

        GroupTag? group = null;
        string? id = null;

        // Парсим группу, если она указана
        if (!string.IsNullOrWhiteSpace(groupPart))
        {
            if (!Enum.TryParse<GroupTag>(groupPart, true, out var parsedGroup))
            {
                return false;
            }

            group = parsedGroup;
        }

        // Парсим ID, если он указан
        if (!string.IsNullOrWhiteSpace(idPart))
        {
            id = idPart;
        }

        // Хотя бы одно значение должно быть задано
        if (group is null && id is null)
        {
            return false;
        }

        result = new TagIdFilter(group, id);
        return true;
    }

    /// <summary>
    ///     Проверяет, соответствует ли данный TagId этому фильтру
    /// </summary>
    /// <param name="tagId">TagId для проверки</param>
    /// <returns>true, если TagId соответствует фильтру</returns>
    public bool Matches(TagId tagId)
    {
        if (Group.HasValue && tagId.Group != Group.Value)
        {
            return false;
        }

        return Id is null || tagId.Id.Contains(Id, StringComparison.OrdinalIgnoreCase);
    }
}
