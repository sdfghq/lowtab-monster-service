using System.Reflection;
using Lowtab.Monster.Service.Application.Interfaces;
using Lowtab.Monster.Service.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Sdf.Platform.EntityFrameworkCore.Interceptors;

namespace Lowtab.Monster.Service.Infrastructure.Persistence;

/// <inheritdoc cref="Lowtab.Monster.Service.Application.Interfaces.IDbContext" />
public class InternalDbContext(
    DbContextOptions<InternalDbContext> options,
    AuditableEntitySaveChangesInterceptor? auditableEntitySaveChangesInterceptor)
    : DbContext(options), IDbContext
{
    public DbSet<TagEntity> Tags { get; set; }
    public DbSet<ArticleEntity> Articles { get; set; }

    /// <inheritdoc />
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        ArgumentNullException.ThrowIfNull(modelBuilder);
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        base.OnModelCreating(modelBuilder);
    }

    /// <inheritdoc />
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        ArgumentNullException.ThrowIfNull(optionsBuilder);
        if (auditableEntitySaveChangesInterceptor is not null)
        {
            optionsBuilder.AddInterceptors(auditableEntitySaveChangesInterceptor);
        }

        base.OnConfiguring(optionsBuilder);
    }
}
