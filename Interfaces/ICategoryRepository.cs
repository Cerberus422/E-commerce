using api.Models;

namespace api.Interfaces;

public interface ICategoryRepository
{
    Task<List<Category>> GetAllAsync(); // showing all Categories
    Task<Category> GetByIdAsync(int categoryId); // showing Category by Id
    Task<Category> CreateAsync(Category categoryModel); // creating Category
}