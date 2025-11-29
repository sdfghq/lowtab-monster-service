using Lowtab.Monster.Service.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Lowtab.Monster.Service.Infrastructure.Persistence.TableConfigurations;

internal class ArticleEntityConfiguration : IEntityTypeConfiguration<ArticleEntity>
{
    public void Configure(EntityTypeBuilder<ArticleEntity> builder)
    {
        builder.HasKey(x => x.Id);
        builder.HasMany(x => x.Tags).WithMany();
    }
}
