using Ecommerce.Domain.Entities;
using Ecommerce.Infra.Data.Utils;

namespace Ecommerce.Infra.Data.Context;

public class AppDbContext : DbContext
{
    public virtual DbSet<Cart> Carts { get; set; }
    public virtual DbSet<CartItem> CartItems { get; set; }

    public virtual DbSet<Product> Products { get; set; }
    public virtual DbSet<ProductCombination> ProductCombinations { get; set; }
    public virtual DbSet<ProductCategory> ProductCategories { get; set; }
    public virtual DbSet<ProductInventory> ProductInventories { get; set; }
    public virtual DbSet<ProductImage> ProductImages { get; set; }
    public virtual DbSet<ProductDiscount> ProductDiscounts { get; set; }
    public virtual DbSet<ProductReview> ProductReviews { get; set; }

    public virtual DbSet<Variant> Variants { get; set; }
    public virtual DbSet<VariantOption> VariantOptions { get; set; }
    public virtual DbSet<ProductVariation> ProductVariations { get; set; }
    public virtual DbSet<ProductVariationOption> ProductVariationOptions { get; set; }

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
}
