using api.Dtos.Product;
using api.Helpers;
using api.Interfaces;
using api.Mappers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace api.Controllers;
[Route("api/product")]
[ApiController]
public class ProductController : ControllerBase
{
    private readonly IProductRepository _productRepo;
    private readonly ICategoryRepository _categoryRepo;

    public ProductController(IProductRepository productRepo,  ICategoryRepository categoryRepo)
    {
        _productRepo = productRepo;
        _categoryRepo = categoryRepo;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll([FromQuery] QueryObject query) // Showing all Products, with filters 
    {
        var products =  await _productRepo.GetAllAsync(query);

        if (!products.Any())
        {
            return NotFound("Nothing was found");
        }

        var productModel = products.Select(p => p.ToProductDto()).ToList();

        return Ok(productModel);
    }
    
    [HttpGet("{productId:int}")]
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
    
    [HttpPost("{categoryId:int}")]
    [Authorize]
    public async Task<IActionResult> Create([FromRoute] int categoryId,[FromBody] CreateProductDto productDto) // Creating Product 
    {
        if (!await _categoryRepo.CategoryExists(categoryId))
        {
            return BadRequest("Category does not exist");
        }

        var productModel = productDto.ToProductFromCreate(categoryId);

        await _productRepo.CreateAsync(productModel);

        var createdProduct = await _productRepo.GetByIdAsync(productModel.Id);
        

        return CreatedAtAction(
            nameof(GetById),
            new { productId = createdProduct!.Id },
            createdProduct.ToProductDto());
    }
    
    [HttpPut("{productId:int}")]
    [Authorize]
    public async Task<IActionResult> Update([FromBody] UpdateProductDto updateProductDto, [FromRoute] int productId) // Updating Product 
    {
        if (!await _categoryRepo.CategoryExists(updateProductDto.CategoryId))
        {
            return BadRequest("Category does not exist");
        }
        
        var product = await _productRepo.UpdateAsync(productId, updateProductDto.ToProductFromUpdate());

        if (product == null)
        {
            return NotFound();
        }

        return Ok(product.ToProductDto());

    }
    
    [HttpDelete("{productId:int}")]
    [Authorize]
    public async Task<IActionResult> Delete([FromRoute] int productId) // deleting Product
    {
        var product = await _productRepo.DeleteAsync(productId);

        if (product == null)
        {
            return NotFound("Product does not exist");
        }

        return NoContent();
    }

    [HttpPatch("{productId:int}")]
    [Authorize]
    public async Task<IActionResult> Patch([FromRoute] int productId, [FromBody] PatchProductDto patchDto) // patching Product
    {
        if (patchDto.CategoryId.HasValue)
        {
            if (!await _categoryRepo.CategoryExists(patchDto.CategoryId.Value))
            {
                return BadRequest("Category does not exist");
            }
        }
        
        var product = await _productRepo.PatchAsync(productId, patchDto);

        if (product == null)
            return NotFound();
        
        return Ok(product.ToProductDto());
    }
    
}