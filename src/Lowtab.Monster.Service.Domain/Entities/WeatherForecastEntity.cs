using Sdf.Platform.EntityFrameworkCore.Abstractions;

namespace Lowtab.Monster.Service.Domain.Entities;

public class WeatherForecastEntity : ITrackedTime
{
    public Guid Id { get; set; }

    public DateOnly Date { get; set; }

    public int TemperatureC { get; set; }

    public string? Summary { get; set; }

    public DateTimeOffset CreatedAt { get; set; }

    public DateTimeOffset UpdatedAt { get; set; }
}
