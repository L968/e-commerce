﻿namespace Ecommerce.Domain.Entities.ProductEntities;

public sealed class ProductCategory : AuditableEntity
{
    public int Id { get; private set; }
    public Guid Guid { get; private init; }
    public string Name { get; private set; } = "";
    public string? Description { get; private set; }

    private ProductCategory() {}

    public ProductCategory(string name, string? description)
    {
        Guid = Guid.NewGuid();
        Name = name;
        Description = description;
    }

    public void Update(string name, string? description)
    {
        Name = name;
        Description = description;
    }
}
