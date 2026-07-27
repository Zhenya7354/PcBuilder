using Microsoft.EntityFrameworkCore;
using PcBuilder.Data.Seeding.Interfaces;
using PcBuilder.Entities;
using PcBuilder.Enums;

namespace PcBuilder.Data.Seeding.Seeders;

public class CpuCoolerSeeder : IDataSeeder
{
    public async Task SeedAsync(PcDbContext context)
    {
        var brands = await context.Brand.ToDictionaryAsync(b => b.Name);

        var cpuCoolers = new List<CpuCoolerEntity>
        {
            new CpuCoolerEntity
            {
                Name = "Cooler Master Hyper 212 Black Edition",
                CoolerType = CoolerType.Air,
                SocketsSupported = new List<PcSocketType> { PcSocketType.AM5, PcSocketType.AM4 },
                FanCount = 1,
                FanSizeMm = 120,
                MaxTdpWatts = 150,
                HeightMm = 159,
                ColorScheme = ColorScheme.RGB,
                NoiseLevelDb = 36.0,
                Currency = Currency.USD,
                Price = 39.99m,
                BrandId = brands["Cooler Master"].Id,
                RadiatorSizeMm = 225
            },
            new CpuCoolerEntity
            {
                Name = "Thermalright AIO 240",
                CoolerType = CoolerType.Liquid,
                SocketsSupported = new List<PcSocketType> { PcSocketType.LGA1700, PcSocketType.LGA1851, PcSocketType.LGA1200 },
                FanCount = 2,
                FanSizeMm = 140,
                MaxTdpWatts = 280,
                HeightMm = null,
                ColorScheme = ColorScheme.RGB,
                NoiseLevelDb = 21.0,
                Currency = Currency.USD,
                Price = 149.99m,
                BrandId = brands["Thermalright"].Id,
                RadiatorSizeMm = 280
            },
            new CpuCoolerEntity
            {
                Name = "Thermalright Assassin X 120 Refined SE",
                CoolerType = CoolerType.Air,
                SocketsSupported = new List<PcSocketType> { PcSocketType.AM4, PcSocketType.AM5, PcSocketType.LGA1700 },
                FanCount = 1,
                FanSizeMm = 120,
                MaxTdpWatts = 150,
                HeightMm = 148,
                ColorScheme = ColorScheme.NonRGB,
                NoiseLevelDb = 25.6,
                Currency = Currency.USD,
                Price = 19.99m,
                BrandId = brands["Thermalright"].Id,
                RadiatorSizeMm = null
            },
            new CpuCoolerEntity
            {
                Name = "Thermalright Peerless Assassin 120 SE",
                CoolerType = CoolerType.Air,
                SocketsSupported = new List<PcSocketType> { PcSocketType.AM4, PcSocketType.AM5, PcSocketType.LGA1700, PcSocketType.LGA1851 },
                FanCount = 2,
                FanSizeMm = 120,
                MaxTdpWatts = 265,
                HeightMm = 155,
                ColorScheme = ColorScheme.NonRGB,
                NoiseLevelDb = 25.6,
                Currency = Currency.USD,
                Price = 39.99m,
                BrandId = brands["Thermalright"].Id,
                RadiatorSizeMm = null
            }
        };

        await context.CpuCooler.AddRangeAsync(cpuCoolers);
        await context.SaveChangesAsync();
    }
}
