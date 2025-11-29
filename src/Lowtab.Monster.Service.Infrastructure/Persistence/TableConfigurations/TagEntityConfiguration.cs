using Lowtab.Monster.Service.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Lowtab.Monster.Service.Infrastructure.Persistence.TableConfigurations;

internal class TagEntityConfiguration : IEntityTypeConfiguration<TagEntity>
{
    public void Configure(EntityTypeBuilder<TagEntity> builder)
    {
        builder.HasKey(x => new { x.Id, x.Group });
    }
}
