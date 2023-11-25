using Ecommerce.Domain.Entities;
using Ecommerce.Domain.Repositories;

namespace Ecommerce.Infra.Data.Repositories;

public sealed class UnitOfWork : IUnitOfWork
{
    private readonly AppDbContext _dbContext;

    public UnitOfWork(AppDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public Task SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        AddTimestamps();
        return _dbContext.SaveChangesAsync(cancellationToken);
    }

    private void AddTimestamps()
    {
        var entities = _dbContext
            .ChangeTracker
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
