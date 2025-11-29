using System.Text.Json.Serialization;

namespace Lowtab.Monster.Service.Contracts.GroupTags;

/// <summary>
///     Группа тега
/// </summary>
[JsonConverter(typeof(JsonStringEnumConverter))]
public enum GroupTagEnum
{
    Map = 1,
    Side = 2,
    Grenade = 3,
    DestinationPoint = 4,
    StartPoint = 5
}
