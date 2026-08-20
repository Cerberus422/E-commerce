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
}