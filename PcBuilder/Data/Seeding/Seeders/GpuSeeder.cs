using Microsoft.EntityFrameworkCore;
using PcBuilder.Data.Seeding.Interfaces;
using PcBuilder.Entities;
using PcBuilder.Enums;
using System.Diagnostics;

namespace PcBuilder.Data.Seeding.Seeders;

public class GpuSeeder : IDataSeeder
{
    public async Task SeedAsync(PcDbContext context)
    {
        if (await context.Gpu.AnyAsync())
            return;
        var brands = await context.Brand.ToDictionaryAsync(b => b.Name);

        var gpus = new List<GpuEntity>
        {
            new GpuEntity
            {
                Name = "NVIDIA GeForce RTX 4090",
                Brand = brands["NVIDIA"],
                GpuChip = "AD102",
                GpuInterface = GpuInterface.PCle5x16,
                VramGb = 24,
                VramType = "GDDR6X",
                BaseClockMhz = 2235,
                BoostClockMhz = 2520,
                MemoryBusBits = 384,
                MemoryBandwithGb = 1008,
                TdpWatts = 450,
                RecommendedPsuWattage = 850,
                PowerConnectors = "1x 16-pin",
                OutputHdmi = 1,
                OutputDp = 3,
                CardLengthMm = 304,
                CardSlots = 3.5,
                HasRgb = true,
                Price = 1599.99m
            },
            new GpuEntity
            {
                Name = "AMD Radeon RX 7900 XTX",
                Brand = brands["AMD"],
                GpuChip = "Navi 31",
                GpuInterface = GpuInterface.PCle4x16,
                VramGb = 24,
                VramType = "GDDR6",
                BaseClockMhz = 1900,
                BoostClockMhz = 2500,
                MemoryBusBits = 384,
                MemoryBandwithGb = 960,
                TdpWatts = 355,
                RecommendedPsuWattage = 750,
                PowerConnectors = "2x 8-pin",
                OutputHdmi = 1,
                OutputDp = 3,
                CardLengthMm = 267,
                CardSlots = 2.5,
                HasRgb = false,
                Price = 999.99m
            }   
        };
        await context.Gpu.AddRangeAsync(gpus);
        await context.SaveChangesAsync();
    }
}
