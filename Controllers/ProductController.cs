using api.Dtos.Product;
using api.Interfaces;
using api.Mappers;
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
    public async Task<IActionResult> GetAll() // Showing all Products
    {
        var products =  await _productRepo.GetAllAsync();

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
    
    [HttpGet("category/{categoryId:int}")]
    public async Task<IActionResult> GetProductsByCategoryId([FromRoute] int categoryId) // Showing Products By CategoryId
    {
        var products =  await _productRepo.GetProductsByCategoryIdAsync(categoryId);

        if (!products.Any())
        {
            return NotFound("Nothing in here yet");
        }
        
        var productModel = products.Select(p => p.ToProductDto()).ToList();

        return Ok(productModel);
    }
    
    [HttpPost("{Id:int}")]
    public async Task<IActionResult> Create([FromRoute] int Id,[FromBody] CreateProductDto productDto) // Creating Product 
    {
        if (!await _categoryRepo.CategoryExists(Id))
        {
            return BadRequest("Category does not exist");
        }

        var productModel = productDto.ToProductFromCreate(Id);

        await _productRepo.CreateAsync(productModel);

        var createdProduct = await _productRepo.GetByIdAsync(productModel.Id);
        

        return CreatedAtAction(
            nameof(GetById),
            new { productId = createdProduct!.Id },
            createdProduct.ToProductDto());
    }
    
    [HttpPut("{Id:int}")]
    public async Task<IActionResult> Update([FromBody] UpdateProductDto updateProductDto, [FromRoute] int Id) // Updating Product 
    {
        if (!await _categoryRepo.CategoryExists(updateProductDto.CategoryId))
        {
            return BadRequest("Category does not exist");
        }
        
        var product = await _productRepo.UpdateAsync(Id, updateProductDto.ToProductFromUpdate());

        if (product == null)
        {
            return NotFound();
        }

        return Ok(product.ToProductDto());

    }
    
    [HttpDelete("{Id:int}")]
    public async Task<IActionResult> Delete([FromRoute] int Id) // deleting Product
    {
        var product = await _productRepo.DeleteAsync(Id);

        if (product == null)
        {
            return NotFound("Product does not exist");
        }

        return NoContent();
    }

    [HttpPatch("{Id:int}")]
    public async Task<IActionResult> Patch([FromRoute] int Id, [FromBody] PatchProductDto patchDto) // patching Product
    {
        if (patchDto.CategoryId.HasValue)
        {
            if (!await _categoryRepo.CategoryExists(patchDto.CategoryId.Value))
            {
                return BadRequest("Category does not exist");
            }
        }
        
        var product = await _productRepo.PatchAsync(Id, patchDto);

        if (product == null)
            return NotFound();
        
        return Ok(product.ToProductDto());
    }
    
}