using api.Interfaces;
using api.Mappers;
using Microsoft.AspNetCore.Mvc;

namespace api.Controllers;
[Route("api/product")]
[ApiController]
public class ProductController : ControllerBase
{
    private readonly IProductRepository _productRepo;

    public ProductController(IProductRepository productRepo)
    {
        _productRepo = productRepo;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll() // Showing all Products
    {
        var products =  await _productRepo.GetAllAsync();

        var productModel = products.Select(p => p.ToProductDto()).ToList();

        return Ok(productModel);
    }
    
    [HttpGet("{productId}")]
    public async Task<IActionResult> GetById([FromRoute] int productId) // Showing Product By Id
    {
        var product =  await _productRepo.GetByIdAsync(productId);

        if (product == null)
        {
            return NotFound();
        }
        
        var productModel = product.ToProductDto();

        return Ok(productModel);
    }
    
    [HttpGet("category/{categoryId}")]
    public async Task<IActionResult> GetProductsByCategoryId([FromRoute] int categoryId) // Showing Products By CategoryId
    {
        var products =  await _productRepo.GetProductsByCategoryIdAsync(categoryId);

        if (!products.Any())
        {
            return NotFound();
        }
        
        var productModel = products.Select(p => p.ToProductDto()).ToList();

        return Ok(productModel);
    }
    
}