using System.ComponentModel.DataAnnotations;

namespace api.Dtos.CartItem;

public class CreateCartItemDto
{
    [Required]
    public int ProductId { get; set; }
    
    [Required]
    [Range(1,999)]
    public int Quantity { get; set; }
}