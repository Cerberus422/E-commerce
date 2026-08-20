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
    public async Task<IActionResult> GetById([FromRoute] int productId) // Showing Products By Id
    {
        var products =  await _productRepo.GetById(productId);

        if (products == null)
        {
            return NotFound();
        }
        
        var productModel = products.ToProductDto();

        return Ok(productModel);
    }
}