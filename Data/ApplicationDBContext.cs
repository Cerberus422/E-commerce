using api.Models;
using Microsoft.AspNetCore.Identity;
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

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
        
        List<IdentityRole> roles = new List<IdentityRole>
        {
            new IdentityRole
            {
                Id = "Admin",
                Name = "Admin",
                NormalizedName = "ADMIN",
                ConcurrencyStamp = "Admin"
            },
            new IdentityRole
            {
                Id = "User",
                Name = "User",
                NormalizedName = "USER",
                ConcurrencyStamp = "User"
            }
        };
        builder.Entity<IdentityRole>().HasData(roles);
    }
}