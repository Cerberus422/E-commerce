using api.Models;

namespace api.Interfaces;

public interface ICategoryRepository
{
    Task<List<Category>> GetAllAsync(); // showing all Categories
    Task<List<Category>> CreateAsync(); // creating Category
}