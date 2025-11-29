using Lowtab.Monster.Service.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Lowtab.Monster.Service.Application.Interfaces;

public interface IDbContext
{
    DbSet<WeatherForecastEntity> WeatherForecasts { get; }
    Task<int> SaveChangesAsync(CancellationToken ct);
}
