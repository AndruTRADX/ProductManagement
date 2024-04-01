using Microsoft.EntityFrameworkCore;
using ProductManagement.Context;
using ProductManagement.Models;

namespace ProductManagement.Services;

public class CategoryService(ProductContext productContext) : ICategoryService
{
    private readonly ProductContext _dbContext = productContext;

    public async Task<Category?> Get(Guid id)
    {
        return await _dbContext.Categories
            .Include(c => c.Products)
            .FirstOrDefaultAsync(c => c.CategoryID == id);
    }

    public async Task<IEnumerable<Category>> GetAll()
    {
        return await _dbContext.Categories.ToListAsync();
    }

    public async Task Save(Category category)
    {
        await _dbContext.Categories.AddAsync(category);
        await _dbContext.SaveChangesAsync();
    }

    public async Task Update(Guid id, Category category)
    {
        var currentCategory = await _dbContext.Categories.FindAsync(id);

        if (currentCategory != null)
        {
            currentCategory.Name = category.Name;
            currentCategory.Description = category.Description;
            currentCategory.Color = category.Color;

            await _dbContext.SaveChangesAsync();
        }
    }

    public async Task Delete(Guid id)
    {
        var currentCategory = await _dbContext.Categories.FindAsync(id);

        if (currentCategory != null)
        {
            _dbContext.Categories.Remove(currentCategory);
            await _dbContext.SaveChangesAsync();
        }
    }
}

public interface ICategoryService
{
    Task<IEnumerable<Category>> GetAll();
    Task<Category?> Get(Guid id);
    Task Save(Category category);
    Task Update(Guid id, Category category);
    Task Delete(Guid id);
}
