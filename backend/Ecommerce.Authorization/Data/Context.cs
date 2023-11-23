using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace Ecommerce.Authorization.Data;

public class Context : IdentityDbContext<CustomIdentityUser, IdentityRole<int>, int>
{
    public Context(DbContextOptions<Context> options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        var admin = new CustomIdentityUser
        {
            Id = 1,
            UserName = "admin",
            NormalizedUserName = "ADMIN",
            Email = "admin@admin.com",
            NormalizedEmail = "ADMIN@ADMIN.COM",
            EmailConfirmed = true,
            Name = "Admin",
            SecurityStamp = Guid.NewGuid().ToString(),
        };

        var testUser = new CustomIdentityUser
        {
            Id = 2,
            UserName = "test",
            NormalizedUserName = "TEST",
            Email = "test@test.com",
            NormalizedEmail = "TEST@TEST.COM",
            EmailConfirmed = true,
            Name= "Tester",
            SecurityStamp= Guid.NewGuid().ToString(),
        };

        var hasher = new PasswordHasher<CustomIdentityUser>();

        builder.Entity<IdentityRole<int>>().HasData(new IdentityRole<int> { Id = 1, Name = "admin", NormalizedName = "ADMIN" });
        builder.Entity<IdentityRole<int>>().HasData(new IdentityRole<int> { Id = 2, Name = "regular", NormalizedName = "REGULAR" });

        admin.PasswordHash = hasher.HashPassword(admin, Config.AdminInfoPassword);
        builder.Entity<CustomIdentityUser>().HasData(admin);
        builder.Entity<IdentityUserRole<int>>().HasData(new IdentityUserRole<int> { UserId = 1, RoleId = 1 });

        testUser.PasswordHash = hasher.HashPassword(testUser, "12345678");
        builder.Entity<CustomIdentityUser>().HasData(testUser);
        builder.Entity<IdentityUserRole<int>>().HasData(new IdentityUserRole<int> { UserId = 2, RoleId = 2 });
    }
}
