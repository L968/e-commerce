﻿using Bogus;
using Ecommerce.Domain.Entities.ProductEntities;
using Ecommerce.Domain.Entities.VariantEntities;

namespace Ecommerce.Domain.UnitTests.SampleData;

public static class FakeProducts
{
    public static ProductCategory GetProductCategory()
    {
        var faker = new Faker<ProductCategory>()
            .CustomInstantiator(f => ProductCategory.Create(
                name: f.Commerce.Categories(1)[0],
                description: f.Random.String(),
                variantIds: [f.Random.Guid()]
            ).Value);

        return faker.Generate();
    }

    public static IEnumerable<ProductCombination> GetProductCombination(Guid productId, IEnumerable<VariantOption> variantOptions, int count = 1)
    {
        var faker = new Faker<ProductCombination>()
            .CustomInstantiator(f => ProductCombination.Create(
                productId: productId,
                variantOptions: variantOptions,
                sku: f.Commerce.Ean13(),
                price: f.Random.Decimal(10, 10000),
                stock: f.Random.Int(1, 1000),
                length: f.Random.Float(1, 10),
                width: f.Random.Float(1, 10),
                height: f.Random.Float(1, 10),
                weight: f.Random.Float(1, 10),
                imagePaths: [f.Image.PicsumUrl(), f.Image.PicsumUrl()]
             ).Value);

        return faker.Generate(count);
    }

    public static Product GetProducts()
    {
        return GetProducts(1).ElementAt(0);
    }

    public static IEnumerable<Product> GetProducts(int count = 1)
    {
        var faker = new Faker();

        var productFaker = new Faker<Product>()
            .CustomInstantiator(f => Product.Create(
                name: f.Commerce.Product(),
                description: f.Commerce.ProductDescription(),
                active: f.Random.Bool(),
                visible: f.Random.Bool(),
                productCategoryId: GetProductCategory().Id
             ).Value);

        var products = productFaker.Generate(count);

        foreach (Product product in products)
        {
            for (int i = 0; i < 3; i++)
            {
                var variantOptions = FakeVariants.GetVariantOptions();

                product.AddCombination(
                    variantOptions,
                    sku: faker.Commerce.Ean13(),
                    price: faker.Random.Decimal(10, 10000),
                    stock: faker.Random.Int(1, 1000),
                    length: faker.Random.Float(1, 10),
                    width: faker.Random.Float(1, 10),
                    height: faker.Random.Float(1, 10),
                    weight: faker.Random.Float(1, 10),
                    imagePaths: [faker.Image.PicsumUrl(), faker.Image.PicsumUrl()]
                );

                foreach (ProductVariantOption productVariantOption in product.VariantOptions)
                {
                    Utils.SetPrivateProperty(productVariantOption, "VariantOption", variantOptions.FirstOrDefault(vo => vo.Id == productVariantOption.VariantOptionId)!);
                }
            }
        }

        return products;
    }
}
