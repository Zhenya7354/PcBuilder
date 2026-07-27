using Microsoft.EntityFrameworkCore;
using PcBuilder.Data.Seeding.Interfaces;
using PcBuilder.Entities;
using PcBuilder.Enums;

namespace PcBuilder.Data.Seeding.Seeders;

public class RamSeeder : IDataSeeder
{
    public async Task SeedAsync(PcDbContext context)
    {
        var brands = await context.Brand.ToDictionaryAsync(b => b.Name);

        var rams = new List<RamEntity>()
        {
            new RamEntity
            {
                Name = "Corsair Vengeance LPX 16GB (2x8GB) DDR4-3200",
                MemoryType = MemoryType.DDR4,
                CapacityGb = 16,
                KitCount = 2,
                SpeedMhz = 3200,
                CasLatency = 16,
                Voltage = 1.35,
                ColorScheme = ColorScheme.RGB,
                HasEcc = false,
                HeightMm = 34,
                Currency = Currency.USD,
                Price = 89.99m,
                BrandId = brands["Corsair"].Id
            },
            new RamEntity
            {
                Name = "G.Skill Trident Z RGB 32GB (2x16GB) DDR4-3600",
                MemoryType = MemoryType.DDR4,
                CapacityGb = 32,
                KitCount = 2,
                SpeedMhz = 3600,
                CasLatency = 18,
                Voltage = 1.35,
                ColorScheme = ColorScheme.RGB,
                HasEcc = false,
                HeightMm = 44,
                Currency = Currency.USD,
                Price = 199.99m,
                BrandId = brands["G.Skill"].Id
            },
            new RamEntity
            {
                Name = "Kingston HyperX Fury 16GB (2x8GB) DDR4-2666",
                MemoryType = MemoryType.DDR4,
                CapacityGb = 16,
                KitCount = 2,
                SpeedMhz = 2666,
                CasLatency = 15,
                Voltage = 1.2,
                ColorScheme = ColorScheme.ARGB,
                HasEcc = false,
                HeightMm = 31,
                Currency = Currency.USD,
                Price = 74.99m,
                BrandId = brands["Kingston"].Id
            },
            new RamEntity
            {
                Name = "Kingston Fury Beast 16GB (2x8GB) DDR4-3200",
                MemoryType = MemoryType.DDR4,
                CapacityGb = 8,
                KitCount = 2,
                SpeedMhz = 3200,
                CasLatency = 16,
                Voltage = 1.35,
                ColorScheme = ColorScheme.NonRGB,
                HasEcc = false,
                HeightMm = 34,
                Currency = Currency.USD,
                Price = 49.99m,
                BrandId = brands["Kingston"].Id
            },
            new RamEntity
            {
                Name = "G.Skill Flare X5 32GB (2x16GB) DDR5-6000",
                MemoryType = MemoryType.DDR5,
                CapacityGb = 16,
                KitCount = 2,
                SpeedMhz = 6000,
                CasLatency = 36,
                Voltage = 1.35,
                ColorScheme = ColorScheme.NonRGB,
                HasEcc = false,
                HeightMm = 33,
                Currency = Currency.USD,
                Price = 139.99m,
                BrandId = brands["G.Skill"].Id
            }
        };
        var existingNames = await context.Ram.Select(r => r.Name).ToListAsync();
        var newRams = rams
            .Where(r => !existingNames.Contains(r.Name))
            .ToList();

        if (newRams.Count == 0)
            return;

        await context.Ram.AddRangeAsync(newRams);
        await context.SaveChangesAsync();
    }
}
