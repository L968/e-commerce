using NetArchTest.Rules;

namespace Ecommerce.Architecture.Tests;

public class ArchitectureTests
{
    private const string DomainNamespace = "Ecommerce.Domain";
    private const string ApplicationNamespace = "Ecommerce.Application";
    private const string InfraDataNamespace = "Ecommerce.Infra.Data";
    private const string InfraIocNamespace = "Ecommerce.Infra.Ioc";
    private const string ApiNamespace = "Ecommerce.API";

    [Fact]
    public void Domain_Should_Not_HaveDependencyOnOtherProjects()
    {
        // Arrange
        var otherProjects = new[]
        {
            ApplicationNamespace,
            InfraDataNamespace,
            InfraIocNamespace,
            ApiNamespace
        };

        // Act
        var result = Types
            .InCurrentDomain()
            .That()
            .ResideInNamespace(DomainNamespace)
            .ShouldNot()
            .HaveDependencyOnAny(otherProjects)
            .GetResult()
            .IsSuccessful;

        // Assert
        Assert.True(result);
    }

    [Fact]
    public void Application_Should_Not_HaveDependencyOnOtherProjects()
    {
        // Arrange
        var otherProjects = new[]
        {
            InfraDataNamespace,
            InfraIocNamespace,
            ApiNamespace
        };

        // Act
        var result = Types
            .InCurrentDomain()
            .That()
            .ResideInNamespace(ApplicationNamespace)
            .ShouldNot()
            .HaveDependencyOnAny(otherProjects)
            .GetResult()
            .IsSuccessful;

        // Assert
        Assert.True(result);
    }

    [Fact]
    public void InfraData_Should_Not_HaveDependencyOnOtherProjects()
    {
        // Arrange
        var otherProjects = new[]
        {
            ApplicationNamespace,
            InfraIocNamespace,
            ApiNamespace
        };

        // Act
        var result = Types
            .InCurrentDomain()
            .That()
            .ResideInNamespace(InfraDataNamespace)
            .ShouldNot()
            .HaveDependencyOnAny(otherProjects)
            .GetResult()
            .IsSuccessful;

        // Assert
        Assert.True(result);
    }

    [Fact]
    public void Api_Should_Not_HaveDependencyOnOtherProjects()
    {
        // Arrange
        var otherProjects = new[]
        {
            DomainNamespace,
            InfraDataNamespace
        };

        // Act
        var result = Types
            .InCurrentDomain()
            .That()
            .ResideInNamespace(ApiNamespace)
            .ShouldNot()
            .HaveDependencyOnAny(otherProjects)
            .GetResult()
            .IsSuccessful;

        // Assert
        Assert.True(result);
    }
}
