﻿namespace Ecommerce.Domain.Entities.ProductEntities;

public sealed class Product : AuditableEntity
{
    public Guid Id { get; private set; }
    public string Name { get; private set; } = "";
    public string Description { get; private set; } = "";
    public string Sku { get; private set; } = "";
    public decimal Price { get; private set; }
    public bool Active { get; private set; }
    public bool Visible { get; private set; }
    public float Length { get; private set; }
    public float Width { get; private set; }
    public float Height { get; private set; }
    public float Weight { get; private set; }
    public int ProductCategoryId { get; private set; }
    public ProductInventory Inventory { get; private set; }
    public List<ProductImage> Images { get; set; } = new();

    public ProductCategory? Category { get; set; }

    private Product() { }

    public Product(
        string name,
        string description,
        string sku,
        decimal price,
        bool active,
        bool visible,
        float length,
        float width,
        float height,
        float weight,
        int productCategoryId,
        int stock,
        List<ProductImage> images
    )
    {
        Id = Guid.NewGuid();
        Name = name;
        Description = description;
        Sku = sku;
        Price = price;
        Active = active;
        Visible = visible;
        Length = length;
        Width = width;
        Height = height;
        Weight = weight;
        ProductCategoryId = productCategoryId;
        Inventory = new ProductInventory(Id, stock);
        Images = images;
    }

    public void Update(
        string name,
        string description,
        string sku,
        decimal price,
        bool active,
        bool visible,
        float length,
        float width,
        float height,
        float weight,
        int productCategoryId
    )
    {
        Name = name;
        Description = description;
        Sku = sku;
        Price = price;
        Active = active;
        Visible = visible;
        Length = length;
        Width = width;
        Height = height;
        Weight = weight;
        ProductCategoryId = productCategoryId;
    }
}