using api.Models;

namespace api.Interfaces;

public interface ICartRepository
{
    Task<Cart> GetCartByUserIdAsync(string UserId); // Showing Cart by userId
    Task<CartItem> AddCartItemAsync(CartItem cartItem); // Create CartItem
    Task<CartItem?> UpdateCartItemAsync(int cartItemId, int quantity); // Update CartItem
    
}