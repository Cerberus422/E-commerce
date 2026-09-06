using api.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace api.Data;

public class ApplicationDBContext : IdentityDbContext<AppUser>
{
    public ApplicationDBContext(DbContextOptions dbContextOptions) : base(dbContextOptions)
    {
    }
    
    public DbSet<Category> Categories { get; set; }
    public DbSet<Product> Products { get; set; }
}