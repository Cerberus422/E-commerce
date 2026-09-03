using System.ComponentModel.DataAnnotations;
using api.Dtos.Category;

namespace api.Dtos.Product;

public class CreateProductDto
{
    [Required]
    [MinLength(2, ErrorMessage = "Name must be 5 characters")]
    [MaxLength(50, ErrorMessage = "Name cannot be over 280 characters")]
    public string Name { get; set; } = string.Empty;
    
    [Required]
    [MinLength(2, ErrorMessage = "Description must be 5 characters")]
    [MaxLength(50, ErrorMessage = "Description cannot be over 400 characters")]
    public string Description { get; set; } = string.Empty;
    
    [Required]
    [Range(1,9999.99)]
    public decimal Price { get; set; }
    
    [Required]
    [Range(0,999)]
    public int Quantity { get; set; }
    
}