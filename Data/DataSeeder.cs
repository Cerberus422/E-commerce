using api.Models;
using Microsoft.AspNetCore.Identity;

namespace api.Data;

public class DataSeeder
{
    
    
    public static async Task SeedAdminAsync(IServiceProvider services, IConfiguration configuration)
    {
        
        var userManager = services.GetRequiredService<UserManager<AppUser>>();
        
        var adminEmail = configuration["Admin:Email"];
        var adminPassword = configuration["Admin:Password"];
        
        var admin = await userManager.FindByEmailAsync(adminEmail);
        
        if (admin == null)
        {
            admin = new AppUser
            {
                UserName = adminEmail,
                Email = adminEmail
            };
            
            var result = await userManager.CreateAsync(admin, adminPassword);
        
            if (result.Succeeded)
            {
                await userManager.AddToRoleAsync(admin, "Admin");
            }
        }
        
    }
}