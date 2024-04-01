using Microsoft.EntityFrameworkCore;
using ProductManagement.Models;

namespace ProductManagement.Context;

public class ProductContext(DbContextOptions<ProductContext> options) : DbContext(options)
{
    public DbSet<Product> Products { get; set; }
    public DbSet<Category> Categories { get; set; }
    public DbSet<Brand> Brands { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        ConfigureProductEntity(modelBuilder);
        ConfigureCategoryEntity(modelBuilder);
        ConfigureBrandEntity(modelBuilder);
    }

    private static void ConfigureProductEntity(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Product>(entity =>
        {
            entity.ToTable("Products");
            entity.HasKey(p => p.ProductID);
            entity.Property(p => p.Title).IsRequired().HasMaxLength(100);
            entity.Property(p => p.Description).IsRequired().HasMaxLength(500);
            entity.Property(p => p.Image).IsRequired();
            entity.Property(p => p.Stock).IsRequired();
            entity.HasOne(p => p.Category).WithMany(c => c.Products).HasForeignKey(p => p.CategoryID).OnDelete(DeleteBehavior.Cascade);
            entity.HasOne(p => p.Brand).WithMany(c => c.Products).HasForeignKey(p => p.BrandID).OnDelete(DeleteBehavior.Cascade);
        });
    }

    private static void ConfigureCategoryEntity(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Category>(entity =>
        {
            entity.ToTable("Categories");
            entity.HasKey(c => c.CategoryID);
            entity.Property(c => c.Name).IsRequired().HasMaxLength(50);
            entity.Property(c => c.Description).IsRequired().HasMaxLength(200);
            entity.Property(c => c.Color).IsRequired();
        });
    }

    private static void ConfigureBrandEntity(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Brand>(entity =>
        {
            entity.ToTable("Brands");
            entity.HasKey(b => b.BrandID);
            entity.Property(b => b.Name).IsRequired().HasMaxLength(50);
            entity.Property(b => b.Logo).IsRequired();
        });
    }
}
