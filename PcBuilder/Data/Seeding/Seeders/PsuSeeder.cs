using Microsoft.EntityFrameworkCore;
using PcBuilder.Data.Seeding.Interfaces;
using PcBuilder.Entities;
using PcBuilder.Enums;

namespace PcBuilder.Data.Seeding.Seeders;

public class PsuSeeder : IDataSeeder
{
    public async Task SeedAsync(PcDbContext context)
    {
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
            },
            new PsuEntity
            {
                Name = "Corsair CX650M",
                Brand = brands["Corsair"],
                Wattage = 650,
                Efficiency = PsuRating.Bronze,
                Modularity = PsuModular.SemiModular,
                AtxVersion = "ATX12V v2.4",
                Has16Pin = false,
                EpsConnectors = 2,
                SataConnectors = 5,
                Pcie8PinConnectors = 4,
                FanSizeMm = 120,
                LengthMm = 140,
                Currency = Currency.USD,
                Price = 69.99m
            },
            new PsuEntity
            {
                Name = "Seasonic Vertex GX-850",
                Brand = brands["Seasonic"],
                Wattage = 850,
                Efficiency = PsuRating.Gold,
                Modularity = PsuModular.FullyModular,
                AtxVersion = "ATX 3.0",
                Has16Pin = true,
                EpsConnectors = 4,
                SataConnectors = 8,
                Pcie8PinConnectors = 6,
                FanSizeMm = 135,
                LengthMm = 160,
                Currency = Currency.USD,
                Price = 139.99m
            }
        };
        var existingNames = await context.Psu.Select(p => p.Name).ToListAsync();
        var newPsus = psus
            .Where(p => !existingNames.Contains(p.Name))
            .ToList();

        if (newPsus.Count == 0)
            return;

        await context.Psu.AddRangeAsync(newPsus);
        await context.SaveChangesAsync();
    }
}
