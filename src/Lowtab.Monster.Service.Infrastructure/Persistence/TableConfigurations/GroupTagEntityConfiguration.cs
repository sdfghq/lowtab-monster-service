using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Lowtab.Monster.Service.Domain.Entities;

namespace Lowtab.Monster.Service.Infrastructure.Persistence.TableConfigurations;

internal class GroupTagEntityConfiguration : IEntityTypeConfiguration<GroupTagEntity>
{
    public void Configure(EntityTypeBuilder<GroupTagEntity> builder)
    {
        builder.HasKey(x => x.Id);
        builder.HasMany(x => x.Tags).WithMany(x => x.Groups);
    }
}
