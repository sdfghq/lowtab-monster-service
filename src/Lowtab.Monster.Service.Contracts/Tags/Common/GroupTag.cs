using System.Text.Json.Serialization;

namespace Lowtab.Monster.Service.Contracts.Tags.Common;

/// <summary>
///     Группа тега
/// </summary>
[JsonConverter(typeof(JsonStringEnumConverter))]
public enum GroupTag
{
    None = 0,
    Map = 1,
    Side = 2,
    Grenade = 3,
    StartPoint = 4,
    DestinationPoint = 5
}
