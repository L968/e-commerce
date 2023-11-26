using Microsoft.EntityFrameworkCore;

namespace Ecommerce.Order.API.Context.EntitiesConfiguration
{
    public class OrderItemConfiguration : IEntityTypeConfiguration<Domain.Entities.OrderEntities.OrderItem>
    {
        public void Configure(Microsoft.EntityFrameworkCore.Metadata.Builders.EntityTypeBuilder<Domain.Entities.OrderEntities.OrderItem> builder)
        {
            builder.HasKey(oi => oi.Id);
            builder.Property(oi => oi.ProductUnitPrice).HasPrecision(65, 2);
            builder.Property(oi => oi.ProductDiscount).HasPrecision(65, 2);
        }
    }
}
