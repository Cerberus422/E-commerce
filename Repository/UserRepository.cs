using api.Data;
using api.Helpers;
using api.Interfaces;
using api.Models;
using Microsoft.EntityFrameworkCore;

namespace api.Repository;

public class UserRepository : IUserRepository
{
    private readonly ApplicationDBContext _context;

    public UserRepository(ApplicationDBContext context)
    {
        _context = context;
    }
    
    public async Task<List<AppUser>> GetUsersAsync(UserQueryObject query)
    {
        var users = _context.Users.AsQueryable();

        if (!string.IsNullOrWhiteSpace(query.SortBy))
        {
            if (query.SortBy.Equals("username", StringComparison.OrdinalIgnoreCase))
            {
                users = query.IsDescending
                    ? users.OrderByDescending(u => u.UserName)
                    : users.OrderBy(u => u.UserName);
            }
            
            else if  (query.SortBy.Equals("email", StringComparison.OrdinalIgnoreCase))
            {
                users = query.IsDescending
                    ? users.OrderByDescending(u => u.Email)
                    : users.OrderBy(u => u.Email);
            }
        }
        
        var skipNumber = (query.PageNumber - 1) * query.PageSize;

        return await users.Skip(skipNumber).Take(query.PageSize).ToListAsync();
    }
}