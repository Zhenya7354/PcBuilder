namespace PcBuilder.Data.Seeding.Interfaces;

public interface IDbSeeder
{
    public Task SeedDataAsync(IServiceProvider services);
}
