using System.ComponentModel.DataAnnotations;

namespace api.Dtos.Product;

public class PatchProductDto
{
    [MinLength(2, ErrorMessage = "Name must be 2 characters")]
    [MaxLength(50, ErrorMessage = "Name cannot be over 50 characters")]
    public string? Name { get; set; } 
    
    [MinLength(2, ErrorMessage = "Description must be 2 characters")]
    [MaxLength(400, ErrorMessage = "Description cannot be over 400 characters")]
    public string? Description { get; set; }
    
    [Range(1,9999.99)]
    public decimal? Price { get; set; }
    
    [Range(0,999)]
    public int? Quantity { get; set; }
    
    public int? CategoryId { get; set; }
}