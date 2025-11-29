using Lowtab.Monster.Service.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Lowtab.Monster.Service.Infrastructure.Persistence.TableConfigurations;

internal class WeatherForecastEntityConfiguration : IEntityTypeConfiguration<WeatherForecastEntity>
{
    public void Configure(EntityTypeBuilder<WeatherForecastEntity> builder)
    {
        builder.HasKey(ax => ax.Id);
        builder.HasIndex(x => x.Date);
        builder.Property(x => x.Summary).HasMaxLength(500);
    }
}
