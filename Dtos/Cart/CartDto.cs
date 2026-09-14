using api.Dtos.CartItem;

namespace api.Dtos.Cart;

public class CartDto
{
    public int Id { get; set; }

    public List<CartItemDto> CartItems { get; set; }
}