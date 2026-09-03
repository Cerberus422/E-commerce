using api.Dtos.Category;
using api.Dtos.Product;
using api.Models;

namespace api.Mappers;

public static class ProductMapper
{
    public static ProductDto ToProductDto(this Product productModel)
    {
        return new ProductDto
        {
            Id = productModel.Id,
            Name = productModel.Name,
            Description  = productModel.Description,
            Price = productModel.Price,
            Quantity = productModel.Quantity,
            
            Category = productModel.Category.ToCategoryDto()
        };
    }

    public static Product ToProductFromCreate(this CreateProductDto productDto, int categoryId)
    {
        return new Product
        {
            Name = productDto.Name,
            Description = productDto.Description,
            Price = productDto.Price,
            Quantity = productDto.Quantity,
            
            CategoryId = categoryId
        };
    }
    
    public static Product ToProductFromUpdate(this UpdateProductDto productModel)
    {
        return new Product
        {
            Name = productModel.Name,
            Description = productModel.Description,
            Price = productModel.Price,
            Quantity = productModel.Quantity,
            CategoryId = productModel.CategoryId
        };
    }
}