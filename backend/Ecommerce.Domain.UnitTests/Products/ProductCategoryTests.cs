using Bogus;
using Ecommerce.Domain.Entities.ProductEntities;
using Ecommerce.Domain.Errors;

namespace Ecommerce.Domain.UnitTests.Products;

public class ProductCategoryTests
{
    private readonly Faker<ProductCategory> _productCategoryFaker;

    public ProductCategoryTests()
    {
        _productCategoryFaker = new Faker<ProductCategory>()
            .CustomInstantiator(f => ProductCategory.Create(
                name: f.Commerce.Categories(1)[0],
                description: f.Lorem.Sentence(),
                variantIds: [f.Random.Guid(), f.Random.Guid()]
            ).Value);
    }

    [Fact]
    public void Create_WithValidData_ShouldReturnProductCategory()
    {
        // Arrange
        var productCategory = _productCategoryFaker.Generate();

        // Act
        var result = ProductCategory.Create(
            name: productCategory.Name,
            description: productCategory.Description,
            variantIds: [Guid.NewGuid(), Guid.NewGuid()]
        );

        // Assert
        Assert.True(result.IsSuccess);
        Assert.NotNull(result.Value);

        var createdProductCategory = result.Value;
        Assert.NotEmpty(createdProductCategory.Name);
        Assert.NotEmpty(createdProductCategory.Description!);
        Assert.NotEmpty(createdProductCategory.Variants);
    }

    [Fact]
    public void Create_WithEmptyVariantList_ShouldReturnError()
    {
        // Arrange
        var name = "CategoryName";
        var description = "CategoryDescription";
        var variantIds = new List<Guid>();

        // Act
        var result = ProductCategory.Create(name, description, variantIds);

        // Assert
        Assert.True(result.IsFailed);
        Assert.Equal(DomainErrors.ProductCategory.EmptyVariantList, result.Errors[0]);
    }

    [Fact]
    public void Update_WithValidData_ShouldSucceed()
    {
        // Arrange
        var productCategory = _productCategoryFaker.Generate();
        var newName = "NewCategoryName";
        var newDescription = "NewCategoryDescription";
        var newVariantIds = new List<Guid> { Guid.NewGuid(), Guid.NewGuid() };

        // Act
        var result = productCategory.Update(newName, newDescription, newVariantIds);

        // Assert
        Assert.True(result.IsSuccess);
        Assert.Equal(newName, productCategory.Name);
        Assert.Equal(newDescription, productCategory.Description);
        Assert.Equal(newVariantIds, productCategory.Variants.Select(v => v.VariantId).ToList());
    }

    [Fact]
    public void Update_WithEmptyVariantList_ShouldReturnError()
    {
        // Arrange
        var productCategory = _productCategoryFaker.Generate();
        var newName = "NewCategoryName";
        var newDescription = "NewCategoryDescription";
        var newVariantIds = new List<Guid>();

        // Act
        var result = productCategory.Update(newName, newDescription, newVariantIds);

        // Assert
        Assert.False(result.IsSuccess);
        Assert.Equal(DomainErrors.ProductCategory.EmptyVariantList, result.Errors[0]);
    }
}