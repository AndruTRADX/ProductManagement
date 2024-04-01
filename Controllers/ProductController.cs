using Microsoft.AspNetCore.Mvc;
using ProductManagement.Context;
using ProductManagement.Models;
using ProductManagement.Services;

namespace ProductManagement.Controllers;

[ApiController]
[Route("[controller]")]
public class ProductController(IProductService productService, ProductContext dbContext) : ControllerBase
{
    private readonly IProductService _productService = productService;
    readonly ProductContext _dbContext = dbContext;

    [HttpGet]
    public async Task<IActionResult> GetAll(int page = 1, int pageSize = 10)
    {
        var products = await _productService.GetAll(page, pageSize);
        return Ok(products);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> Get(Guid id)
    {
        var product = await _productService.Get(id);
        if (product == null)
        {
            return NotFound();
        }
        return Ok(product);
    }

    [HttpPost]
    public async Task<IActionResult> Post([FromBody] Product product)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        await _productService.Save(product);
        return CreatedAtAction(nameof(Get), new { id = product.ProductID }, product);
    }

    [HttpPatch("{id}")]
    public async Task<IActionResult> Patch(Guid id, [FromBody] Product product)
    {
        if (id != product.ProductID)
        {
            return BadRequest();
        }

        await _productService.Update(id, product);
        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(Guid id)
    {
        var existingProduct = await _productService.Get(id);
        if (existingProduct == null)
        {
            return NotFound();
        }

        await _productService.Delete(id);
        return NoContent();
    }

    [HttpGet()]
    [Route("/ensure-created")]
    public IActionResult CreateDatabase()
    {
        return Ok(_dbContext.Database.EnsureCreated());
    }
}