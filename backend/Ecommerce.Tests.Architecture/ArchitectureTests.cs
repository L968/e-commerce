using NetArchTest.Rules;

namespace Ecommerce.Tests.Architecture;

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
        var assembly = AppDomain.CurrentDomain.Load("Ecommerce.Application");

        var otherProjects = new[]
        {
            ApplicationNamespace,
            InfraDataNamespace,
            InfraIocNamespace,
            ApiNamespace
        };

        // Act
        var testResult = Types
            .InAssembly(assembly)
            .ShouldNot()
            .HaveDependencyOnAll(otherProjects)
            .GetResult();

        // Assert
        Assert.True(testResult.IsSuccessful);
    }
}