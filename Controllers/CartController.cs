using api.Extensions;
using api.Interfaces;
using api.Mappers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace api.Controllers;
[Route("api/cart")]
[ApiController]
public class CartController : ControllerBase
{
    private readonly ICartRepository _cartRepo;
    
    public CartController(ICartRepository cartRepo)
    {
        _cartRepo = cartRepo;
    }

    [HttpGet]
    [Authorize]
    public async Task<IActionResult> ShowCart()
    {
        var userId = User.GetUserId();

        var cart = await _cartRepo.GetCartByUserId(userId);

        return Ok(cart.ToCartDto());
    }
}