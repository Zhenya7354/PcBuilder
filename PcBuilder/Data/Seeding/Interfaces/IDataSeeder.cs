namespace PcBuilder.Data.Seeding.Interfaces;

public interface IDataSeeder
{
    public Task SeedAsync(PcDbContext context);
}
