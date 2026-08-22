using api.Data;
using api.Interfaces;
using api.Models;
using Microsoft.EntityFrameworkCore;

namespace api.Repository;

public class ProductRepository : IProductRepository
{
    private readonly ApplicationDBContext _context;

    public ProductRepository(ApplicationDBContext context)
    {
        _context = context;
    }


    public async Task<List<Product>> GetAllAsync()
    {
       return await _context.Products
           .Include(p => p.Category)
           .ToListAsync();
    }

    public async Task<Product?> GetByIdAsync(int productId)
    {
        return await _context.Products
            .Include(p => p.Category)
            .FirstOrDefaultAsync(p => p.Id == productId);
    }

    public async Task<List<Product>> GetProductsByCategoryIdAsync(int categoryId)
    {
        return await _context.Products
            .Include(p => p.Category)
            .Where(p => p.CategoryId == categoryId).ToListAsync();
    }

    public async Task<Product> CreateAsync(Product productModel)
    {
        await _context.Products.AddAsync(productModel);
        await _context.SaveChangesAsync();

        return productModel;
    }

    public async Task<Product?> UpdateAsync(int id, Product updateDto)
    {
        var existingProduct = await _context.Products.FindAsync(id);

        if (existingProduct == null)
            return null;

        existingProduct.Name = updateDto.Name;
        existingProduct.Description = updateDto.Description;
        existingProduct.Price = updateDto.Price;
        existingProduct.Quantity = updateDto.Quantity;
        existingProduct.CategoryId = updateDto.CategoryId;
        
        await _context.SaveChangesAsync();

        return await _context.Products
            .Include(p => p.Category)
            .FirstOrDefaultAsync(p => p.Id == id);

    }

    public async Task<Product?> DeleteAsync(int id)
    {
        var existingProduct = await _context.Products.FindAsync(id);

        if (existingProduct == null)
            return null;

        _context.Products.Remove(existingProduct);
        await _context.SaveChangesAsync();

        return existingProduct;
    }
}