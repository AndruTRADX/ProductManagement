using Microsoft.EntityFrameworkCore;
using ProductManagement.Context;
using ProductManagement.Models;

namespace ProductManagement.Services;

public class BrandService(ProductContext productContext) : IBrandService
{
    private readonly ProductContext _dbContext = productContext;

    public async Task<Brand?> Get(Guid id)
    {
        return await _dbContext.Brands
            .Include(b => b.Products)
            .FirstOrDefaultAsync(b => b.BrandID == id);
    }

    public async Task<IEnumerable<Brand>> GetAll()
    {
        return await _dbContext.Brands.ToListAsync();
    }

    public async Task Save(Brand brand)
    {
        await _dbContext.Brands.AddAsync(brand);
        await _dbContext.SaveChangesAsync();
    }

    public async Task Update(Guid id, Brand brand)
    {
        var currentBrand = await _dbContext.Brands.FindAsync(id);

        if (currentBrand != null)
        {
            currentBrand.Name = brand.Name;
            currentBrand.Logo = brand.Logo;

            await _dbContext.SaveChangesAsync();
        }
    }

    public async Task Delete(Guid id)
    {
        var currentBrand = await _dbContext.Brands.FindAsync(id);

        if (currentBrand != null)
        {
            _dbContext.Brands.Remove(currentBrand);
            await _dbContext.SaveChangesAsync();
        }
    }

    public async Task AddProductToBrand(Guid brandId, Guid productId)
{
    var brand = await _dbContext.Brands
        .Include(b => b.Products)
        .FirstOrDefaultAsync(b => b.BrandID == brandId);

    if (brand != null)
    {
        var existingProduct = await _dbContext.Products.FindAsync(productId);

        if (existingProduct != null)
        {
            brand.Products.Add(existingProduct);
            await _dbContext.SaveChangesAsync();
        }
        else
        {
            throw new Exception("Product not found.");
        }
    }
}
}
public interface IBrandService
{
    Task<IEnumerable<Brand>> GetAll();
    Task<Brand?> Get(Guid id);
    Task Save(Brand brand);
    Task Update(Guid id, Brand brand);
    Task Delete(Guid id);
    Task AddProductToBrand(Guid brandId, Guid productId);
}
