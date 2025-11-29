using Lowtab.Monster.Service.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Lowtab.Monster.Service.Application.Interfaces;

public interface IDbContext
{
    DbSet<TagEntity> Tags { get; set; }
    DbSet<ArticleEntity> Articles { get; set; }
    Task<int> SaveChangesAsync(CancellationToken ct);
}
