using api.Models;

namespace api.Interfaces;

public interface ICategoryRepository
{
    Task<List<Category>> GetAllAsync(); // showing all Categories
    Task<Category?> GetByIdAsync(int categoryId); // showing Category by Id
    Task<Category> CreateAsync(Category categoryModel); // creating Category
    Task<Category?> UpdateAsync(int categoryId, Category updateDto); // updating Category
    Task<Category?> DeleteAsync(int categoryId); // delete Category
    Task<bool> CategoryExists(int categoryId); // checking if Category exists
}