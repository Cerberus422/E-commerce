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
}