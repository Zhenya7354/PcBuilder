using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using PcBuilder.Data.Seeding.Interfaces;

namespace PcBuilder.Data.Seeding;

public class DbSeeder(PcDbContext context, IEnumerable<IDataSeeder> seeders, IIdentityRolesSeeder identityRolesSeeder) : IDbSeeder
{
    private readonly PcDbContext _context = context;
    private readonly IEnumerable<IDataSeeder> _seeders = seeders;
    private readonly IIdentityRolesSeeder _identityRolesSeeder = identityRolesSeeder;

    public async Task SeedDataAsync(IServiceProvider services)
    {
        await _context.Database.MigrateAsync();

        await _identityRolesSeeder.SeedRolesAsync(services);
        foreach (var seeder in _seeders)
        {
            await seeder.SeedAsync(_context);
        }
    }
}
