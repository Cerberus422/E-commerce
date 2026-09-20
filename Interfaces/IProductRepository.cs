using api.Dtos.Product;
using api.Helpers;
using api.Models;

namespace api.Interfaces;

public interface IProductRepository
{
     Task<List<Product>> GetAllAsync(QueryObject query); // showing all Products, with filters 
     Task<Product?> GetByIdAsync(int productId); // showing Product by Id
     Task<Product> CreateAsync(Product productModel); // creating Product
     Task<Product?> UpdateAsync(int productId,Product updateDto); // updating Product
     Task<Product?> DeleteAsync(int productId); // delete Product
     Task<Product?> PatchAsync(int productId,PatchProductDto patchDto); // patch Product
     Task<bool> ProductExists(int productId); // Checking if Product exists
}