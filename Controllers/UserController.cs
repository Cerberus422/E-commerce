using api.Extensions;
using api.Helpers;
using api.Interfaces;
using api.Mappers;
using api.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace api.Controllers;

[Route("api/user")]
[ApiController]
[Authorize(Roles = "Admin")]
public class UserController : ControllerBase
{
    private readonly UserManager<AppUser> _userManager;
    private readonly IUserRepository _userRepository;
    
    public UserController(UserManager<AppUser> userManager, IUserRepository userRepository)
    {
        _userManager = userManager;
        _userRepository = userRepository;
    }
    
    [HttpGet]
    public async Task<IActionResult> GetUsers([FromQuery] UserQueryObject query) // get all users info
    {

        var users = await _userRepository.GetUsersAsync(query);

        var userDtos = users
            .Select(u => u.ToAdminUserDto())
            .ToList();

        return Ok(userDtos);
    }
    
    [HttpGet("{Id}")] 
    public async Task<IActionResult> GetUserById([FromRoute] string Id) // get user info by Id
    {
        var user = await _userManager.FindByIdAsync(Id);

        if (user == null)
        {
            return NotFound();
        }

        return Ok(user.ToAdminUserDto());
    }


    [HttpDelete("{Id}")]
    public async Task<IActionResult> DeleteUser([FromRoute] string Id) // delete user by Id
    {
        var user = await _userManager.FindByIdAsync(Id);

        if (user == null)
        {
            return NotFound();
        }

        var result = await _userManager.DeleteAsync(user);

        if (!result.Succeeded)
        {
            return BadRequest(result.Errors);
        }
        
        return NoContent();
    }
}