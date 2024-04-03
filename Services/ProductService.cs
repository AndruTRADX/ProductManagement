using Microsoft.EntityFrameworkCore;
using ProductManagement.Context;
using ProductManagement.Models;

namespace ProductManagement.Services;

public class ProductService(ProductContext productContext, ICategoryService categoryService, IBrandService brandService) : IProductService
{
    private readonly ProductContext _dbContext = productContext;
    private readonly ICategoryService _categoryService = categoryService;
    private readonly IBrandService _brandService = brandService;

    public async Task<Product?> Get(Guid id)
    {
        return await _dbContext.Products.FindAsync(id);
    }

    public async Task<IEnumerable<Product>> GetAll(int page, int pageSize)
    {
        var totalCount = _dbContext.Products.Count();
        var totalPages = (int)Math.Ceiling((decimal)totalCount / pageSize);
        var productsPerPage = await _dbContext.Products.Skip((page - 1) * pageSize).Take(pageSize).ToListAsync();

        return productsPerPage;
    }

    public async Task Save(Product product)
    {
        if (product.CategoryID == Guid.Empty)
        {
            throw new ArgumentException("Category ID must be provided.");
        }
        if (product.BrandID == Guid.Empty)
        {
            throw new ArgumentException("Brand ID must be provided.");
        }

        await _dbContext.Products.AddAsync(product);
        await _dbContext.SaveChangesAsync();

        await _categoryService.AddProductToCategory(product.CategoryID, product.ProductID);
        await _brandService.AddProductToBrand(product.BrandID, product.ProductID);
    }

    public async Task Update(Guid id, Product product)
    {
        var currentProduct = await _dbContext.Products.FindAsync(id);

        if (currentProduct != null)
        {
            currentProduct.Title = product.Title;
            currentProduct.Description = product.Description;
            currentProduct.Image = product.Image;
            currentProduct.Price = product.Price;
            currentProduct.CategoryID = product.CategoryID;
            currentProduct.BrandID = product.BrandID;

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
    Task<IEnumerable<Product>> GetAll(int page, int pageSize);
    Task<Product?> Get(Guid id);
    Task Save(Product product);
    Task Update(Guid id, Product product);
    Task Delete(Guid id);
}
