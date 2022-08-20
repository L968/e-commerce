using FluentAssertions;
using Ecommerce.Services;
using Ecommerce.Controllers;
using Microsoft.AspNetCore.Mvc;
using Ecommerce.Models;
using Ecommerce.Data;
using Moq;
using Microsoft.EntityFrameworkCore;
using Ecommerce.Data.DTO;

namespace Ecommerce.Tests.Systems.Controllers
{
    public class TestProductCategoryController
    {
        [Fact]
        public void Get_OnSuccess_ReturnsStatusCode200()
        {
            // Arrange
            var controller = GetController(new List<ProductCategory>());

            // Act
            var result = (OkObjectResult)controller.Get();

            // Assert
            result.StatusCode.Should().Be(200);
        }

        [Fact]
        public void GetById_OnSuccess_ReturnsStatusCode200()
        {
            // Arrange
            var dataList = new List<ProductCategory>()
            {
                new ProductCategory()
                {
                    Id = 1,
                    Guid = new Guid("47b3621f-70b8-4d7a-8f05-6f0aee2511f3"),
                    Name = "Shirts",
                    Description = "Lorem Ipsum",
                    CreatedAt = DateTime.Now,
                    UpdatedAt = DateTime.Now,
                }
            };

            var controller = GetController(dataList);

            // Act
            var result = (OkObjectResult)controller.Get(new Guid("47b3621f-70b8-4d7a-8f05-6f0aee2511f3"));

            // Assert
            result.StatusCode.Should().Be(200);
        }

        [Fact]
        public void Create_OnSuccess_ReturnsStatusCode201()
        {
            // Arrange
            var productCategory = new CreateProductCategoryDto()
            {
                Name = "Pants",
                Description = "Lorem Ipsum",
            };

            var controller = GetController(new List<ProductCategory>());

            // Act
            var result = (CreatedAtActionResult)controller.Create(productCategory);

            // Assert
            result.StatusCode.Should().Be(201);
        }

        private ProductCategoryController GetController(List<ProductCategory> dataList)
        {
            var dataDbSet = Helpers.EntityFramework.ListToDbSet(dataList);
            var mockContext = new Mock<Context>(new DbContextOptions<Context>());
            mockContext.Setup(m => m.ProductCategories).Returns(dataDbSet);

            var service = new ProductCategoryService(mockContext.Object);
            return new ProductCategoryController(service);
        }
    }
}