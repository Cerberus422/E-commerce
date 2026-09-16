using api.Data;
using api.Interfaces;
using api.Models;
using Microsoft.EntityFrameworkCore;

namespace api.Repository;

public class CartRepository : ICartRepository
{
    private readonly ApplicationDBContext _context;

    public CartRepository(ApplicationDBContext context)
    {
        _context = context;
    }

    public async Task<Cart> GetCartByUserIdAsync(string UserId)
    {
        var cart = await _context
            .Carts.Include(c => c.CartItems)
            .ThenInclude(ci => ci.Product)
            .FirstOrDefaultAsync(c => c.AppUserId == UserId);

        if (cart == null)
        {
            cart = new Cart
            {
                AppUserId = UserId
            };

            _context.Carts.Add(cart);
            await _context.SaveChangesAsync();
        }

        return cart;
    }
    
    public async Task<CartItem> AddCartItemAsync(CartItem cartItem)
    {
        await _context.CartItems.AddAsync(cartItem);
        await _context.SaveChangesAsync();

        return cartItem;
    }

    public async Task<CartItem?> UpdateCartItemAsync(int cartItemId, int quantity)
    {
        var existingItem = await  _context.CartItems
            .FindAsync(cartItemId);

        if (existingItem == null)
            return null;
        
        existingItem.Quantity = quantity;
        
        await _context.SaveChangesAsync();

        return existingItem;
    }
}