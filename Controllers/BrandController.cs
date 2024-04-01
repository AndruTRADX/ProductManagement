using Microsoft.AspNetCore.Mvc;
using ProductManagement.Models;
using ProductManagement.Services;

namespace ProductManagement.Controllers;

[ApiController]
[Route("[controller]")]
public class BrandController(IBrandService brandService) : ControllerBase
{
    private readonly IBrandService _brandService = brandService;

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var brands = await _brandService.GetAll();
        return Ok(brands);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> Get(Guid id)
    {
        var brand = await _brandService.Get(id);
        if (brand == null)
        {
            return NotFound();
        }
        return Ok(brand);
    }

    [HttpPost]
    public async Task<IActionResult> Post([FromBody] Brand brand)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        await _brandService.Save(brand);
        return CreatedAtAction(nameof(Get), new { id = brand.BrandID }, brand);
    }

    [HttpPatch("{id}")]
    public async Task<IActionResult> Patch(Guid id, [FromBody] Brand brand)
    {
        if (id != brand.BrandID)
        {
            return BadRequest();
        }

        await _brandService.Update(id, brand);
        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(Guid id)
    {
        var existingBrand = await _brandService.Get(id);
        if (existingBrand == null)
        {
            return NotFound();
        }

        await _brandService.Delete(id);
        return NoContent();
    }
}
