using api.Data;
using api.Interfaces;
using api.Models;
using Microsoft.EntityFrameworkCore;

namespace api.Repository;

public class CategoryRepository : ICategoryRepository
{
    private readonly ApplicationDBContext _context;

    public CategoryRepository(ApplicationDBContext context)
    {
        _context = context;
    }
    
    public async Task<List<Category>> GetAllAsync()
    {
        return await _context.Categories.ToListAsync();
    }

    public async Task<Category?> GetByIdAsync(int categoryId)
    {
        return await _context.Categories.FindAsync(categoryId);
    }

    public async Task<Category> CreateAsync(Category categoryModel)
    {
        await _context.Categories.AddAsync(categoryModel);
        await _context.SaveChangesAsync();

        return categoryModel;
    }

    public async Task<Category?> UpdateAsync(int categoryId, Category updateDto)
    {
        var existingCategory = await _context.Categories.FindAsync(categoryId);

        if (existingCategory == null)
        {
            return null;
        }

        existingCategory.Name = updateDto.Name;

        await _context.SaveChangesAsync();

        return existingCategory;
    }

    public async Task<Category?> DeleteAsync(int categoryId)
    {
        var categoryModel = await _context.Categories.FindAsync(categoryId);

        if (categoryModel == null)
        {
            return null;
        }

        _context.Remove(categoryModel);
        await _context.SaveChangesAsync();

        return categoryModel;
    }

    public  Task<bool> CategoryExists(int categoryId)
    {
        return  _context.Categories.AnyAsync(c => c.Id == categoryId);
    }
}