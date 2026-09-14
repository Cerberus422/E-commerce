using api.Models;

namespace api.Interfaces;

public interface ICartRepository
{
    Task<Cart> GetCartByUserId(string UserId); // Showing Cart by userId
}