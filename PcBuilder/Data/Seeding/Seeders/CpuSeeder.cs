using Microsoft.EntityFrameworkCore;
using PcBuilder.Data.Seeding.Interfaces;
using PcBuilder.Entities;
using PcBuilder.Enums;
using System.Net.Sockets;

namespace PcBuilder.Data.Seeding.Seeders;

public class CpuSeeder : IDataSeeder
{
    public async Task SeedAsync(PcDbContext context)
    {
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
                Currency = Currency.USD,
                Price = 449.99m
                
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
                Currency = Currency.USD,
                Price = 325.99m
            },
            new CpuEntity
            {
                BrandId = brands["AMD"].Id,
                Name = "Ryzen 5 5600",
                Socket = Enums.PcSocketType.AM4,
                Cores = 6,
                Threads = 12,
                BaseClockGhz = 3.5,
                BoostClockGhz = 4.4,
                TdpWatts = 65,
                ChipsetsSupported = new List<string> { "B550", "X570" },
                MemoryType = Enums.MemoryType.DDR4,
                MaxMemoryGb = 128,
                MaxMemorySpeedMhz = 3600,
                MemoryChannels = 2,
                IntegratedGraphics = false,
                IncludesCooler = true,
                IgpuModel = null,
                LaunchedYear = 2022,
                Currency = Currency.USD,
                Price = 139.99m
            },
            new CpuEntity
            {
                BrandId = brands["AMD"].Id,
                Name = "Ryzen 7 7700X",
                Socket = Enums.PcSocketType.AM5,
                Cores = 8,
                Threads = 16,
                BaseClockGhz = 4.5,
                BoostClockGhz = 5.4,
                TdpWatts = 105,
                ChipsetsSupported = new List<string> { "B650", "X670" },
                MemoryType = Enums.MemoryType.DDR5,
                MaxMemoryGb = 128,
                MaxMemorySpeedMhz = 6000,
                MemoryChannels = 2,
                IntegratedGraphics = true,
                IncludesCooler = false,
                IgpuModel = "Radeon Graphics",
                LaunchedYear = 2022,
                Currency = Currency.USD,
                Price = 319.99m
            }
        };

        var existingNames = await context.Cpu.Select(c => c.Name).ToListAsync();
        var newCpus = cpus
            .Where(c => !existingNames.Contains(c.Name))
            .ToList();

        if (newCpus.Count == 0)
            return;

        await context.Cpu.AddRangeAsync(newCpus);
        await context.SaveChangesAsync();
    }
}
