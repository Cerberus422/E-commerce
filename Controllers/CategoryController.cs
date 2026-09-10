using api.Dtos.Category;
using api.Interfaces;
using api.Mappers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace api.Controllers;
[Route("api/category")]
[ApiController]
public class CategoryController : ControllerBase
{
    private readonly ICategoryRepository _categoryRepo;
    
    public CategoryController(ICategoryRepository categoryRepo)
    {
        _categoryRepo = categoryRepo;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll() // Showing all Categories
    {
        var categories = await _categoryRepo.GetAllAsync();

        var categoryDto = categories.Select(c => c.ToCategoryDto());

        return Ok(categoryDto);
    }
    
    [HttpGet("{categoryId:int}")]
    public async Task<IActionResult> GetById([FromRoute] int categoryId) // Showing Category by Id
    {
        var category = await _categoryRepo.GetByIdAsync(categoryId);

        if (category == null)
        {
            return NotFound();
        }
        
        var categoryDto = category.ToCategoryDto();
        
        return Ok(categoryDto);
    }

    [HttpPost]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> Create([FromBody] CreateCategoryDto categoryDto) // Creating Category 
    {
        var categoryModel = categoryDto.ToCategoryFromCreate();
    
        await _categoryRepo.CreateAsync(categoryModel);

        return CreatedAtAction(nameof(GetById), new { categoryId = categoryModel.Id }, categoryModel.ToCategoryDto());
    }
    
    [HttpPut("{categoryId:int}")]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> Update([FromBody] UpdateCategoryDto updateCategoryDto,[FromRoute] int categoryId) // Updating Category 
    {
        
        var category = await _categoryRepo.UpdateAsync(categoryId, updateCategoryDto.ToCategoryFromUpdate());

        if (category == null)
        {
            return NotFound();
        }

        return Ok(category.ToCategoryDto());
    }
    
    [HttpDelete("{categoryId:int}")]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> Delete([FromRoute] int categoryId) // deleting Category
    {
        var categories = await _categoryRepo.DeleteAsync(categoryId);

        if (categories == null)
        {
            return NotFound("Category does not exist");
        }

        return NoContent();
    }
}