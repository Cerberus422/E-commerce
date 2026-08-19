using System.ComponentModel.DataAnnotations;

namespace api.Dtos.Category;

public class CreateCategoryDto
{
    [Required]
    [MinLength(2, ErrorMessage = "Title must be 5 characters")]
    [MaxLength(50, ErrorMessage = "Title cannot be over 280 characters")]
    public string Name { get; set; } = string.Empty;
}