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
                ColorScheme = ColorScheme.RGB,
                Currency = Currency.USD,
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
                ColorScheme = ColorScheme.RGB,
                Currency = Currency.USD,
                Price = 999.99m
            },
            new GpuEntity
            {
                Name = "AMD Radeon RX 7600 XT",
                Brand = brands["Sapphire"],
                GpuChip = "Navi 33",
                GpuInterface = GpuInterface.PCle4x16,
                VramGb = 16,
                VramType = "GDDR6",
                BaseClockMhz = 1980,
                BoostClockMhz = 2755,
                MemoryBusBits = 128,
                MemoryBandwithGb = 288,
                TdpWatts = 190,
                RecommendedPsuWattage = 550,
                PowerConnectors = "1x 8-pin",
                OutputHdmi = 1,
                OutputDp = 3,
                CardLengthMm = 250,
                CardSlots = 2.5,
                ColorScheme = ColorScheme.NonRGB,
                Currency = Currency.USD,
                Price = 329.99m
            },
            new GpuEntity
            {
                Name = "NVIDIA GeForce RTX 4070 Ti SUPER",
                Brand = brands["NVIDIA"],
                GpuChip = "AD103",
                GpuInterface = GpuInterface.PCle4x16,
                VramGb = 16,
                VramType = "GDDR6X",
                BaseClockMhz = 2340,
                BoostClockMhz = 2610,
                MemoryBusBits = 256,
                MemoryBandwithGb = 672,
                TdpWatts = 285,
                RecommendedPsuWattage = 700,
                PowerConnectors = "1x 16-pin",
                OutputHdmi = 1,
                OutputDp = 3,
                CardLengthMm = 300,
                CardSlots = 3,
                ColorScheme = ColorScheme.RGB,
                Currency = Currency.USD,
                Price = 799.99m
            }   
        };
        var existingNames = await context.Gpu.Select(g => g.Name).ToListAsync();
        var newGpus = gpus
            .Where(g => !existingNames.Contains(g.Name))
            .ToList();

        if (newGpus.Count == 0)
            return;

        await context.Gpu.AddRangeAsync(newGpus);
        await context.SaveChangesAsync();
    }
}
