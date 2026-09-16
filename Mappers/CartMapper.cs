using api.Dtos.Cart;
using api.Dtos.CartItem;
using api.Models;

namespace api.Mappers;

public static class CartMapper
{
    public static CartDto ToCartDto(this Cart cartModel)
    {
        return new CartDto()
        {
            Id = cartModel.Id,
            CartItems = cartModel.CartItems
                .Select(items => items.ToCartItemDto())
                .ToList()
        };
    }
}