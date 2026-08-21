using api.Models;

namespace api.Interfaces;

public interface IProductRepository
{
    public Task<List<Product>> GetAllAsync();
    public Task<Product?> GetByIdAsync(int productId);
    public Task<List<Product>> GetProductsByCategoryIdAsync(int categoryId);
}