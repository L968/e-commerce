using Microsoft.EntityFrameworkCore;

namespace Ecommerce.Order.API.Context.EntitiesConfiguration
{
    public class OrderConfiguration : IEntityTypeConfiguration<Domain.Entities.OrderEntities.Order>
    {
        public void Configure(Microsoft.EntityFrameworkCore.Metadata.Builders.EntityTypeBuilder<Domain.Entities.OrderEntities.Order> builder)
        {
            builder
                .HasMany(o => o.Items)
                .WithOne(oi => oi.Order)
                .HasForeignKey(oi => oi.OrderId);

            builder
                .HasMany(o => o.History)
                .WithOne(oh => oh.Order)
                .HasForeignKey(oh => oh.OrderId);

            builder.HasKey(p => p.Id);
            builder.Property(p => p.TotalAmount).HasPrecision(65, 2);
            builder.Property(p => p.ShippingCost).HasPrecision(65, 2);
            builder.Property(p => p.Discount).HasPrecision(65, 2);
        }
    }
}
