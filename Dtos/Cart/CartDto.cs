using api.Dtos.CartItem;

namespace api.Dtos.Cart;

public class CartDto
{
    public List<CartItemDto> CartItems { get; set; }
}