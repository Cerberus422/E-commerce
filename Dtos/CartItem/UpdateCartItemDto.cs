using System.ComponentModel.DataAnnotations;

namespace api.Dtos.CartItem;

public class UpdateCartItemDto
{
    [Required]
    [Range(1,999)]
    public int Quantity { get; set; }
}