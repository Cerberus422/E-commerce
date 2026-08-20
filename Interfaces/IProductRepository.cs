using api.Models;

namespace api.Interfaces;

public interface IProductRepository
{
    public Task<List<Product>> GetAllAsync();
    public Task<Product?> GetById(int productId);
}