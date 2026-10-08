using api.Helpers;
using api.Models;

namespace api.Interfaces;

public interface IUserRepository
{
    Task<List<AppUser>> GetUsersAsync(UserQueryObject query); // showing all Users, with filters 

}