using api.Dtos.Account;
using api.Models;

namespace api.Mappers;

public static class UserMappe
{
    public static UserDto ToUserDto(this AppUser user)
    {
        return new UserDto
        {
            UserName = user.UserName!,
            Email = user.Email!
        };
    }
    
    public static AdminUserDto  ToAdminUserDto(this AppUser user)
    {
        return new AdminUserDto
        {
            Id = user.Id!,
            UserName = user.UserName!,
            Email = user.Email!
        };
    }
}