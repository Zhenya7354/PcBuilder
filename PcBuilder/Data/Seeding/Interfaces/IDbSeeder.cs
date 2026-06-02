namespace PcBuilder.Data.Seeding.Interfaces;

public interface IDbSeeder
{
    public Task SeedRolesAsync(IServiceProvider services);
    public Task SeedDataAsync();
}
