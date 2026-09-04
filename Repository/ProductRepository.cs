using api.Data;
using api.Dtos.Product;
using api.Helpers;
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


    public async Task<List<Product>> GetAllAsync(QueryObject query)
    {
       var product =  _context.Products
           .Include(p => p.Category)
           .AsQueryable();

       if (!string.IsNullOrWhiteSpace(query.Name))
       {
           product = product
               .Where(p => p.Name.Contains(query.Name));
       }
       
       if (query.CategoryId.HasValue)
       {
           product = product
               .Where(p => p.CategoryId == query.CategoryId);
       }
       
       if (query.InStock == true)
       {
           product = product
               .Where(p => p.Quantity > 0 );
       }

       if (!string.IsNullOrWhiteSpace(query.SortBy))
       {
           if(query.SortBy.Equals("Name", StringComparison.OrdinalIgnoreCase))
           {
               product = query.IsDescending 
                   ? product.OrderByDescending(p => p.Name) 
                   : product.OrderBy(p => p.Name);
           }
           else if (query.SortBy.Equals("Price", StringComparison.OrdinalIgnoreCase))
           {
               product = query.IsDescending 
                   ? product.OrderByDescending(p => p.Price) 
                   : product.OrderBy(p => p.Price);
           }
       }

       var skipNumber = (query.PageNumber - 1) * query.PageSize;

       return await product.Skip(skipNumber).Take(query.PageSize).ToListAsync();

    }

    public async Task<Product?> GetByIdAsync(int productId)
    {
        return await _context.Products
            .Include(p => p.Category)
            .FirstOrDefaultAsync(p => p.Id == productId);
    }

    public async Task<Product> CreateAsync(Product productModel)
    {
        await _context.Products.AddAsync(productModel);
        await _context.SaveChangesAsync();

        return productModel;
    }

    public async Task<Product?> UpdateAsync(int productId, Product updateDto)
    {
        var existingProduct = await _context.Products
            .FindAsync(productId);

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
            .FirstOrDefaultAsync(p => p.Id == productId);
        
    }

    public async Task<Product?> DeleteAsync(int productId)
    {
        var existingProduct = await _context.Products.FindAsync(productId);

        if (existingProduct == null)
            return null;

        _context.Products.Remove(existingProduct);
        await _context.SaveChangesAsync();

        return existingProduct;
    }

    public async Task<Product?> PatchAsync(int productId, PatchProductDto patchDto)
    {
        var product = await _context.Products
            .FirstOrDefaultAsync(p => p.Id == productId);

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
            .FirstOrDefaultAsync(p => p.Id == productId);
    }
}