using Ecommerce.Domain.Entities;
using Ecommerce.Order.API.Utils;
using Microsoft.EntityFrameworkCore;

namespace Ecommerce.Order.API.Context;

public class AppDbContext : DbContext
{
    public virtual DbSet<Domain.Entities.OrderEntities.Order> Orders { get; set; }

    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
        builder.ApplyConfigurationsFromAssembly(typeof(AppDbContext).Assembly);

        foreach (var entity in builder.Model.GetEntityTypes())
        {
            builder.Entity(entity.ClrType).ToTable(entity.ClrType.Name.ToSnakeCase());

            foreach (var property in entity.ClrType.GetProperties().Where(p => p.PropertyType == typeof(Guid?)))
            {
                builder
                    .Entity(entity.ClrType)
                    .Property(property.Name)
                    .HasDefaultValueSql("(uuid())");

                builder
                    .Entity(entity.ClrType)
                    .HasIndex(property.Name)
                    .IsUnique();
            }
        }
    }

    public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        AddTimestamps();
        return base.SaveChangesAsync(cancellationToken);
    }

    private void AddTimestamps()
    {
        var entities =
             ChangeTracker
            .Entries()
            .Where(x => x.Entity is AuditableEntity && (x.State == EntityState.Added || x.State == EntityState.Modified));

        foreach (var entity in entities)
        {
            var now = DateTime.UtcNow;

            if (entity.State == EntityState.Added)
            {
                ((AuditableEntity)entity.Entity).CreatedAt = now;
            }

            ((AuditableEntity)entity.Entity).UpdatedAt = now;
        }
    }
}
