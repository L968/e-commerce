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
            .CustomInstantiator(f => new ProductCategory(
                name: f.Commerce.Categories(1)[0],
                description: f.Lorem.Sentence(),
                variantIds: [f.Random.Guid(), f.Random.Guid()]
            ));
    }

    [Fact]
    public void Create_WithValidData_ShouldReturnProductCategory()
    {
        // Arrange
        var productCategory = _productCategoryFaker.Generate();

        // Act
        var createdProductCategory = new ProductCategory(
            name: productCategory.Name,
            description: productCategory.Description,
            variantIds: [Guid.NewGuid(), Guid.NewGuid()]
        );

        // Assert
        Assert.NotNull(createdProductCategory);
        Assert.NotEmpty(createdProductCategory.Name);
        Assert.NotEmpty(createdProductCategory.Description!);
        Assert.NotEmpty(createdProductCategory.Variants);
    }

    [Fact]
    public void Create_WithEmptyVariantList_ShouldFail()
    {
        // Arrange
        var name = "CategoryName";
        var description = "CategoryDescription";
        var variantIds = new List<Guid>();

        // Act and Assert
        var exception = Assert.Throws<DomainException>(() => new ProductCategory(name, description, variantIds));
        Assert.Contains(DomainErrors.ProductCategory.EmptyVariantList, exception.Errors);
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
        productCategory.Update(newName, newDescription, newVariantIds);

        // Assert
        Assert.Equal(newName, productCategory.Name);
        Assert.Equal(newDescription, productCategory.Description);
        Assert.Equal(newVariantIds, productCategory.Variants.Select(v => v.VariantId).ToList());
    }

    [Fact]
    public void Update_WithEmptyVariantList_ShouldFail()
    {
        // Arrange
        var productCategory = _productCategoryFaker.Generate();
        var newName = "NewCategoryName";
        var newDescription = "NewCategoryDescription";
        var newVariantIds = new List<Guid>();

        // Act and Assert
        var exception = Assert.Throws<DomainException>(() => productCategory.Update(newName, newDescription, newVariantIds));
        Assert.Contains(DomainErrors.ProductCategory.EmptyVariantList, exception.Errors);
    }
}