using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using PcBuilder.Data.Seeding.Interfaces;

namespace PcBuilder.Data.Seeding;

public class DbSeeder(PcDbContext context, IEnumerable<IDataSeeder> seeders) : IDbSeeder
{
    private readonly PcDbContext _context = context;
    private readonly IEnumerable<IDataSeeder> _seeders = seeders;
    public async Task SeedRolesAsync(IServiceProvider services)
    {
        var roleManager = services.GetRequiredService<RoleManager<IdentityRole>>();

        foreach (var role in new[] { "Admin", "User"})
        {
            if(!await roleManager.RoleExistsAsync(role))
                await roleManager.CreateAsync(new IdentityRole(role));
        }
    }
    public async Task SeedDataAsync()
    {
        await _context.Database.MigrateAsync();

        foreach(var seeder in _seeders)
        {
            await seeder.SeedAsync(_context);
        }
    }
}
