using Microsoft.EntityFrameworkCore;
using ProductManagement.Context;
using ProductManagement.Models;

namespace ProductManagement.Services;

public class ProductService(ProductContext productContext) : IProductService
{
    private readonly ProductContext _dbContext = productContext;

    public async Task<Product?> Get(Guid id)
    {
        return await _dbContext.Products.FindAsync(id);
    }

    public async Task<IEnumerable<Product>> GetAll()
    {
        return await _dbContext.Products.ToListAsync();
    }

    public async Task Save(Product product)
    {
        await _dbContext.Products.AddAsync(product);
        await _dbContext.SaveChangesAsync();
    }

    public async Task Update(Guid id, Product product)
    {
        var currentProduct = await _dbContext.Products.FindAsync(id);

        if (currentProduct != null)
        {
            currentProduct.Title = product.Title;
            currentProduct.Description = product.Description;
            currentProduct.Image = product.Image;
            currentProduct.CategoryID = product.CategoryID;

            await _dbContext.SaveChangesAsync();
        }
    }

    public async Task Delete(Guid id)
    {
        var currentProduct = await _dbContext.Products.FindAsync(id);

        if (currentProduct != null)
        {
            _dbContext.Products.Remove(currentProduct);
            await _dbContext.SaveChangesAsync();
        }
    }
}


public interface IProductService
{
    Task<IEnumerable<Product>> GetAll();
    Task<Product?> Get(Guid id);
    Task Save(Product product);
    Task Update(Guid id, Product product);
    Task Delete(Guid id);
}
