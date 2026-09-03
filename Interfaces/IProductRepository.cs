using api.Dtos.Product;
using api.Helpers;
using api.Models;

namespace api.Interfaces;

public interface IProductRepository
{
    public Task<List<Product>> GetAllAsync(QueryObject query); // showing all Products, with filters 
    public Task<Product?> GetByIdAsync(int productId); // showing Product by Id
    public Task<Product> CreateAsync(Product productModel); // creating Product
    public Task<Product?> UpdateAsync(int id,Product updateDto); // updating Product
    public Task<Product?> DeleteAsync(int id); // delete Product
    public Task<Product?> PatchAsync(int id,PatchProductDto patchDto); // patch Product
}