using Ecommerce.Consumers.OrderCheckout.Utils;
using Ecommerce.Domain.Entities;
using Ecommerce.Domain.Entities.OrderEntities;
using Ecommerce.Domain.Entities.ProductEntities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace Ecommerce.Consumers.OrderCheckout.Context;

public class AppDbContext : DbContext
{
    public virtual DbSet<Order> Orders { get; set; }
    public virtual DbSet<Address> Addresses { get; set; }
    public virtual DbSet<ProductCombination> ProductCombinations { get; set; }

    public AppDbContext()
    {
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);

        IConfigurationRoot configuration = builder.Build();

        string? connectionString = configuration.GetConnectionString("Connection");
        var serverVersion = ServerVersion.AutoDetect(connectionString);

        optionsBuilder
            .UseSnakeCaseNamingConvention()
            .UseMySql(connectionString, serverVersion);
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
        builder.ApplyConfigurationsFromAssembly(typeof(AppDbContext).Assembly);

        foreach (var entity in builder.Model.GetEntityTypes())
        {
            builder.Entity(entity.ClrType).ToTable(entity.ClrType.Name.ToSnakeCase());
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
