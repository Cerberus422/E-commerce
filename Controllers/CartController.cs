using api.Dtos.CartItem;
using api.Extensions;
using api.Interfaces;
using api.Mappers;
using api.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace api.Controllers;

[Route("api/cart")]
[ApiController]
public class CartController : ControllerBase
{
    private readonly ICartRepository _cartRepo;
    private readonly IProductRepository _productRepo;

    public CartController(ICartRepository cartRepo, IProductRepository productRepo)
    {
        _cartRepo = cartRepo;
        _productRepo = productRepo;
    }

    [HttpGet]
    [Authorize]
    public async Task<IActionResult> ShowCart() // Showing Cart by userId
    {
        var userId = User.GetUserId();

        var cart = await _cartRepo.GetCartByUserIdAsync(userId);

        return Ok(cart.ToCartDto());
    }

    [HttpPost]
    [Authorize]
    public async Task<IActionResult> AddToCart([FromBody] CreateCartItemDto cartItemDto) // Add cart item to the user's cart
    {
        var product = await _productRepo.GetByIdAsync(cartItemDto.ProductId);

        if (product == null)
            return NotFound("Product Not Found");

        var cart = await _cartRepo.GetCartByUserIdAsync(User.GetUserId());

        var existingItem = cart.CartItems
            .FirstOrDefault(ci => ci.ProductId == product.Id);

        if (existingItem != null)
        {
            if (existingItem.Quantity + cartItemDto.Quantity > product.Quantity)
            {
                return BadRequest("Not enough in stock.");
            }

            await _cartRepo.UpdateCartItemAsync(existingItem.Id, cartItemDto.Quantity + existingItem.Quantity);
        }
        else
        {
            if (cartItemDto.Quantity > product.Quantity)
                return BadRequest("Not enough in stock.");

            var cartItem = cartItemDto.ToCartItemFromCreate(cart.Id);

            await _cartRepo.AddCartItemAsync(cartItem);
        }

        cart = await _cartRepo.GetCartByUserIdAsync(User.GetUserId());

        return Ok(cart.ToCartDto());
    }

    [HttpPut("{cartItemId:int}")]
    [Authorize]
    public async Task<IActionResult> UpdateCart([FromRoute] int cartItemId, [FromBody] UpdateCartItemDto updateCartItemDto)
    {
        var cart = await _cartRepo.GetCartByUserIdAsync(User.GetUserId());
        
        var cartItem = cart.CartItems.FirstOrDefault(ci => ci.Id == cartItemId);
        
        if (cartItem == null)
            return NotFound("Cart Item not found");

        var product = await _productRepo.GetByIdAsync(cartItem.ProductId);
        
        if (product == null)
            return NotFound("Product not found");
        
        if (updateCartItemDto.Quantity > product.Quantity)
            return BadRequest("Not enough in stock.");
        
        await _cartRepo.UpdateCartItemAsync(cartItemId, updateCartItemDto.Quantity);
        
        cart = await _cartRepo.GetCartByUserIdAsync(User.GetUserId());

        return Ok(cart.ToCartDto());
    }

}