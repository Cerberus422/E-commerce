using api.Data;
using api.Dtos.Product;
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
        var existingProduct = await _context.Products
            .Include(p => p.Category)
            .FirstOrDefaultAsync(p => p.Id == id);

        if (existingProduct == null)
            return null;

        existingProduct.Name = updateDto.Name;
        existingProduct.Description = updateDto.Description;
        existingProduct.Price = updateDto.Price;
        existingProduct.Quantity = updateDto.Quantity;
        existingProduct.CategoryId = updateDto.CategoryId;
        
        await _context.SaveChangesAsync();

        return existingProduct;


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

    public async Task<Product?> PatchAsync(int id, PatchProductDto patchDto)
    {
        var product = await _context.Products
            .FirstOrDefaultAsync(p => p.Id == id);

        if (product == null)
            return null;

        if (patchDto.Name != null)
            product.Name = patchDto.Name;
        
        if (patchDto.Description != null)
            product.Description = patchDto.Description;
        
        if (patchDto.Price != null)
            product.Price = patchDto.Price.Value;
        
        if (patchDto.Quantity != null)
            product.Quantity = patchDto.Quantity.Value;
        
        if (patchDto.CategoryId != null)
            product.CategoryId = patchDto.CategoryId.Value;
        
        await _context.SaveChangesAsync();
        
        return await _context.Products
            .Include(p => p.Category)
            .FirstOrDefaultAsync(p => p.Id == id);
    }
}