using Ecommerce.Authorization.Models;
using Ecommerce.Infra.Data.Context;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MySqlConnector;
using Respawn;
using System.Data.Common;

namespace Ecommerce.Application.IntegrationTests;

[SetUpFixture]
public partial class Testing
{
    private static WebApplicationFactory<Program> _factory = null!;
    private static IConfiguration _configuration = null!;
    private static IServiceScopeFactory _scopeFactory = null!;
    private static Respawner _respawner = null!;
    private static int? _currentUserId;
    private static DbConnection Connection = null!;

    [OneTimeSetUp]
    public void RunBeforeAnyTests()
    {
        _factory = new CustomWebApplicationFactory();
        _scopeFactory = _factory.Services.GetRequiredService<IServiceScopeFactory>();
        _configuration = _factory.Services.GetRequiredService<IConfiguration>();
        string connectionString = _configuration.GetConnectionString("TestsConnection")!;

        Connection = new MySqlConnection(connectionString);
        Connection.Open();

        var respawnerOptions = new RespawnerOptions
        {
            DbAdapter = DbAdapter.MySql,
            SchemasToInclude = new[] { "ecommerce_tests" },
            TablesToIgnore = new Respawn.Graph.Table[] { "__EFMigrationsHistory" }
        };

        _respawner = Respawner.CreateAsync(Connection, respawnerOptions).GetAwaiter().GetResult();
    }

    public static async Task<TResponse> SendAsync<TResponse>(IRequest<TResponse> request)
    {
        using var scope = _scopeFactory.CreateScope();

        var mediator = scope.ServiceProvider.GetRequiredService<ISender>();

        return await mediator.Send(request);
    }

    public static async Task SendAsync(IBaseRequest request)
    {
        using var scope = _scopeFactory.CreateScope();

        var mediator = scope.ServiceProvider.GetRequiredService<ISender>();

        await mediator.Send(request);
    }

    public static int? GetCurrentUserId()
    {
        return _currentUserId;
    }

    public static int? RunAsRegularUser()
    {
        _currentUserId = 2;
        return _currentUserId;
        //return await RunAsUserAsync("test@test.com", "12345678", new[] { "regular" });
    }

    public static int? RunAsAdministrator()
    {
        _currentUserId = 1;
        return _currentUserId;
        //return await RunAsUserAsync("admin@admin.com", "Admin123!", new[] { "admin" });
    }

    private static async Task<int> RunAsUserAsync(string userName, string password, string[] roles)
    {
        using var scope = _scopeFactory.CreateScope();

        var userManager = scope.ServiceProvider.GetRequiredService<UserManager<CustomIdentityUser>>();

        var user = new CustomIdentityUser { UserName = userName, Email = userName };

        var result = await userManager.CreateAsync(user, password);

        if (roles.Any())
        {
            var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();

            foreach (var role in roles)
            {
                await roleManager.CreateAsync(new IdentityRole(role));
            }

            await userManager.AddToRolesAsync(user, roles);
        }

        if (result.Succeeded)
        {
            _currentUserId = user.Id;

            return _currentUserId.Value;
        }

        throw new Exception($"Unable to create {userName}.{Environment.NewLine}");
    }

    public static async Task ResetState()
    {
        await _respawner.ResetAsync(Connection);
        _currentUserId = null;
    }


    public static async Task<TEntity?> FindAsync<TEntity>(params object[] keyValues)
        where TEntity : class
    {
        using var scope = _scopeFactory.CreateScope();

        var context = scope.ServiceProvider.GetRequiredService<AppDbContext>();

        return await context.FindAsync<TEntity>(keyValues);
    }

    public static async Task<TEntity> AddAsync<TEntity>(TEntity entity)
        where TEntity : class
    {
        using var scope = _scopeFactory.CreateScope();

        var context = scope.ServiceProvider.GetRequiredService<AppDbContext>();

        context.Add(entity);

        await context.SaveChangesAsync();

        return entity;
    }

    public static async Task<int> CountAsync<TEntity>() where TEntity : class
    {
        using var scope = _scopeFactory.CreateScope();

        var context = scope.ServiceProvider.GetRequiredService<AppDbContext>();

        return await context.Set<TEntity>().CountAsync();
    }

    [OneTimeTearDown]
    public void RunAfterAnyTests()
    {
    }
}