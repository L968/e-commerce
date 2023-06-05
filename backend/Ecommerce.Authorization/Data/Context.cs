﻿using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

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

        var hasher = new PasswordHasher<CustomIdentityUser>();
        admin.PasswordHash = hasher.HashPassword(admin, Config.AdminInfoPassword);

        builder.Entity<CustomIdentityUser>().HasData(admin);
        builder.Entity<IdentityRole<int>>().HasData(new IdentityRole<int> { Id = 1, Name = "admin", NormalizedName = "ADMIN" });
        builder.Entity<IdentityUserRole<int>>().HasData(new IdentityUserRole<int> { UserId = 1, RoleId = 1 });

        builder.Entity<IdentityRole<int>>().HasData(new IdentityRole<int> { Id = 2, Name = "regular", NormalizedName = "REGULAR" });
    }
}