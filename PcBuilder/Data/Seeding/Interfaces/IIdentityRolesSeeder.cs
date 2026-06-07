namespace PcBuilder.Data.Seeding.Interfaces;

public interface IIdentityRolesSeeder
{
    public Task SeedRolesAsync(IServiceProvider services);
}
