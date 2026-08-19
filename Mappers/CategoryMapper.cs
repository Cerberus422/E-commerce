using api.Dtos.Category;
using api.Models;

namespace api.Mappers;

public static class CategoryMapper
{
    public static CategoryDto ToCategoryDto(this Category categoryModel)
    {
        return new CategoryDto
        {
            Id = categoryModel.Id,
            Name = categoryModel.Name
        };
    }
    
    public static Category ToCategoryFromCreate(this CreateCategoryDto categoryModel)
    {
        return new Category
        {
            Name = categoryModel.Name
        };
    }
    
    public static Category ToCategoryFromUpdate(this UpdateCategoryDto categoryModel)
    {
        return new Category
        {
            Name = categoryModel.Name
        };
    }
}