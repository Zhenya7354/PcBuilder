using Microsoft.EntityFrameworkCore;
using PcBuilder.Data.Seeding.Interfaces;
using PcBuilder.Entities;
using PcBuilder.Enums;

namespace PcBuilder.Data.Seeding.Seeders;

public class RamSeeder : IDataSeeder
{
    public async Task SeedAsync(PcDbContext context)
    {
        if (await context.CpuCooler.AnyAsync())
            return;

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
                HasRgb = false,
                HasEcc = false,
                HeightMm = 34,
                PriceUsd = 89.99m,
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
                HasRgb = true,
                HasEcc = false,
                HeightMm = 44,
                PriceUsd = 199.99m,
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
                HasRgb = false,
                HasEcc = false,
                HeightMm = 31,
                PriceUsd = 74.99m,
                BrandId = brands["Kingston"].Id
            }
        };
        await context.Ram.AddRangeAsync(rams);
        await context.SaveChangesAsync();
    }
}
