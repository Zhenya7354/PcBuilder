using Microsoft.EntityFrameworkCore;
using PcBuilder.Data.Seeding.Interfaces;
using PcBuilder.Entities;
using System.Net.Sockets;

namespace PcBuilder.Data.Seeding.Seeders;

public class CpuSeeder : IDataSeeder
{
    public async Task SeedAsync(PcDbContext context)
    {
        if(await context.Cpu.AnyAsync())
            return;

        var brands = await context.Brand.ToDictionaryAsync(b => b.Name);
        var cpus = new List<CpuEntity>
        {
            new CpuEntity
            {
                BrandId = brands["AMD"].Id,
                Name = "Ryzen 7 9800X3D",
                Socket = Enums.PcSocketType.AM5,
                Cores = 8,
                Threads = 16,
                BaseClockGhz = 4.7,
                BoostClockGhz = 5.2,
                TdpWatts = 120,
                ChipsetsSupported = new List<string> { "X670", "B650" },
                MemoryType = Enums.MemoryType.DDR5,
                MaxMemoryGb = 128,
                MaxMemorySpeedMhz = 6400,
                MemoryChannels = 2,
                IntegratedGraphics = false,
                IncludesCooler = false,
                IgpuModel = null,
                LaunchedYear = 2015,
                PriceUsd = 449.99m
                
            },
            new CpuEntity
            {
                BrandId = brands["Intel"].Id,
                Name = "Core Ultra 9 285K",
                Socket = Enums.PcSocketType.LGA1851,
                Cores = 8,
                Threads = 16,
                BaseClockGhz = 4.7,
                BoostClockGhz = 5.2,
                TdpWatts = 120,
                ChipsetsSupported = new List<string> { "X670", "B650" },
                MemoryType = Enums.MemoryType.DDR5,
                MaxMemoryGb = 128,
                MaxMemorySpeedMhz = 6400,
                MemoryChannels = 2,
                IntegratedGraphics = false,
                IncludesCooler = false,
                IgpuModel = null,
                LaunchedYear = 2015,
                PriceUsd = 325.99m
            }
        };

        await context.Cpu.AddRangeAsync(cpus);
        await context.SaveChangesAsync();
    }
}
