using api.Models;

namespace api.Interfaces;

public interface ICategoryRepository
{
    Task<List<Category>> GetAllAsync(); // showing all Categories
    Task<Category?> GetByIdAsync(int categoryId); // showing Category by Id
    Task<Category> CreateAsync(Category categoryModel); // creating Category
    Task<Category?> UpdateAsync(int id, Category updateDto); // updating Category
    Task<Category?> DeleteAsync(int id); // delete Category
    Task<bool> CategoryExists(int id); // checking if Category exists
}