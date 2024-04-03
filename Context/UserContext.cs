using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using ProductManagement.Models;

namespace ProductManagement.Context;

public class UserContext(DbContextOptions<UserContext> options) : IdentityDbContext<User>(options)
{

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        ConfigureUserEntity(modelBuilder);
    }
    private static void ConfigureUserEntity(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<User>(entity =>
        {
            entity.ToTable("Users");
            entity.HasKey(b => b.UserId);
            entity.Property(b => b.Name).IsRequired();
            entity.Property(b => b.UserName).IsRequired();
            entity.Property(b => b.Email).IsRequired();
            entity.Property(b => b.Password).IsRequired();
            entity.Property(b => b.Role).IsRequired().HasDefaultValue("Customer");
        });
    }
}
