using Microsoft.EntityFrameworkCore;

namespace Ecommerce.Order.API.Context.EntitiesConfiguration
{
    public class OrderConfiguration : IEntityTypeConfiguration<Domain.Entities.OrderEntities.Order>
    {
        public void Configure(Microsoft.EntityFrameworkCore.Metadata.Builders.EntityTypeBuilder<Domain.Entities.OrderEntities.Order> builder)
        {
            builder.HasKey(p => p.Id);
            builder.Property(p => p.TotalAmount).HasPrecision(65, 2);
            builder.Property(p => p.ShippingCost).HasPrecision(65, 2);
            builder.Property(p => p.Discount).HasPrecision(65, 2);
        }
    }
}
