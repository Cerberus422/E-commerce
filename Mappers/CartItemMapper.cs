using api.Dtos.Cart;
using api.Dtos.CartItem;
using api.Dtos.Category;
using api.Models;

namespace api.Mappers;

public static class CartItemMapper
{
    public static CartItemDto ToCartItemDto(this CartItem cartItemModel)
    {
        return new CartItemDto
        {
            Id = cartItemModel.Id,
            ProductId = cartItemModel.ProductId,
            ProductName = cartItemModel.Product.Name,
            Price = cartItemModel.Product.Price,
            Quantity = cartItemModel.Quantity,
            TotalPrice = cartItemModel.Product.Price * cartItemModel.Quantity
        };
    }
    
    public static CartItem ToCartItemFromCreate(this CreateCartItemDto cartModel,int cartId)
    {
        return new CartItem
        {
           ProductId = cartModel.ProductId,
           Quantity = cartModel.Quantity,
           CartId = cartId
        };
    }
}