﻿namespace Ecommerce.Infra.Data.EntitiesConfiguration;

public class ProductInventoryConfiguration : IEntityTypeConfiguration<ProductInventory>
{
    public void Configure(EntityTypeBuilder<ProductInventory> builder)
    {
        builder
            .HasOne(pi => pi.Product)
            .WithOne(p => p.Inventory)
            .HasForeignKey<ProductInventory>();
    }
}