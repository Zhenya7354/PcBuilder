using Microsoft.AspNetCore.Identity;
using PcBuilder.Data.Seeding.Interfaces;

namespace PcBuilder.Data.Seeding.Seeders;

public class IdentityRolesSeeder : IIdentityRolesSeeder
{
    public async Task SeedRolesAsync(IServiceProvider services)
    {
        var roleManager = services.GetRequiredService<RoleManager<IdentityRole>>();

        foreach (var role in new[] { "Admin", "User" })
        {
            if (!await roleManager.RoleExistsAsync(role))
                await roleManager.CreateAsync(new IdentityRole(role));
        }
    }
}
