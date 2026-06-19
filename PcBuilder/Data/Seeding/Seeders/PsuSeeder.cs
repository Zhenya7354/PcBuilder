using Microsoft.EntityFrameworkCore;
using PcBuilder.Data.Seeding.Interfaces;
using PcBuilder.Entities;
using PcBuilder.Enums;

namespace PcBuilder.Data.Seeding.Seeders;

public class PsuSeeder : IDataSeeder
{
    public async Task SeedAsync(PcDbContext context)
    {
        if (await context.Psu.AnyAsync())
            return;

        var brands = await context.Brand.ToDictionaryAsync(b => b.Name);

        var psus = new List<PsuEntity>()
        {
            new PsuEntity
            {
                Name = "Corsair RM850x",
                Brand = brands["Corsair"],
                Wattage = 850,
                Efficiency = PsuRating.Gold,
                Modularity = PsuModular.FullyModular,
                AtxVersion = "ATX12V v2.4",
                Has16Pin = false,
                EpsConnectors = 4,
                SataConnectors = 8,
                Pcie8PinConnectors = 6,
                FanSizeMm = 135,
                LengthMm = 160,
                Currency = Currency.USD,
                Price = 129.99m
            },
            new PsuEntity
            {
                Name = "EVGA SuperNOVA 750 G5",
                Brand = brands["EVGA"],
                Wattage = 750,
                Efficiency = PsuRating.Gold,
                Modularity = PsuModular.FullyModular,
                AtxVersion = "ATX12V v2.4",
                Has16Pin = false,
                EpsConnectors = 4,
                SataConnectors = 6,
                Pcie8PinConnectors = 6,
                FanSizeMm = 135,
                LengthMm = 150,
                Currency = Currency.USD,
                Price = 119.99m
            },
            new PsuEntity
            {
                Name = "Seasonic Focus GX-650",
                Brand = brands["Seasonic"],
                Wattage = 650,
                Efficiency = PsuRating.Gold,
                Modularity = PsuModular.FullyModular,
                AtxVersion = "ATX12V v2.4",
                Has16Pin = false,
                EpsConnectors = 4,
                SataConnectors = 6,
                Pcie8PinConnectors = 4,
                FanSizeMm = 120,
                LengthMm = 140,
                Currency = Currency.USD,
                Price = 109.99m
            }
        };
        await context.Psu.AddRangeAsync(psus);
        await context.SaveChangesAsync();
    }
}
