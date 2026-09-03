using api.Dtos.Product;
using api.Helpers;
using api.Models;

namespace api.Interfaces;

public interface IProductRepository
{
    public Task<List<Product>> GetAllAsync(QueryObject query); // showing all Products, with filters 
    public Task<Product?> GetByIdAsync(int productId); // showing Product by Id
    public Task<Product> CreateAsync(Product productModel); // creating Product
    public Task<Product?> UpdateAsync(int productId,Product updateDto); // updating Product
    public Task<Product?> DeleteAsync(int productId); // delete Product
    public Task<Product?> PatchAsync(int productId,PatchProductDto patchDto); // patch Product
}