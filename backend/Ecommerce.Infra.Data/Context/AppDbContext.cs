using Ecommerce.Domain.Entities;
using Ecommerce.Infra.Data.Utils;

namespace Ecommerce.Infra.Data.Context;

public class AppDbContext : DbContext
{
    public virtual DbSet<Product> Products { get; set; }
    public virtual DbSet<ProductCategory> ProductCategories { get; set; }
    public virtual DbSet<ProductInventory> ProductInventories { get; set; }
    public virtual DbSet<ProductImage> ProductImages { get; set; }

    public virtual DbSet<Country> Countries { get; set; }
    public virtual DbSet<State> States { get; set; }
    public virtual DbSet<City> Cities { get; set; }
    public virtual DbSet<Address> Addresses { get; set; }

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

    public override int SaveChanges()
    {
        AddTimestamps();
        return base.SaveChanges();
    }

    private void AddTimestamps()
    {
        var entities = ChangeTracker.Entries()
            .Where(x => x.Entity is BaseEntity && (x.State == EntityState.Added || x.State == EntityState.Modified));

        foreach (var entity in entities)
        {
            var now = DateTime.UtcNow;

            if (entity.State == EntityState.Added)
            {
                ((BaseEntity)entity.Entity).CreatedAt = now;
            }

            ((BaseEntity)entity.Entity).UpdatedAt = now;
        }
    }
}