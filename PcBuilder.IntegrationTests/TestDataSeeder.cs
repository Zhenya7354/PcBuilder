using Microsoft.EntityFrameworkCore;
using PcBuilder.Data;
using PcBuilder.Entities;
using PcBuilder.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace PcBuilder.IntegrationTests;

public class TestDataSeeder
{
    private readonly PcDbContext _dbContext;

    public TestDataSeeder(PcDbContext dbContext)
    {
        _dbContext = dbContext;

    }
    public async Task SeedAsync()
    {
        await SeedBrandsAsync();
        await SeedCpusAsync();
        await _dbContext.SaveChangesAsync();
    }

    private async Task SeedBrandsAsync()
    {
        var needed = new[] { "AMD", "Intel" };
        var existing = await _dbContext.Brand.Where(b => needed.Contains(b.Name)).Select(b => b.Name).ToListAsync();
        var toAdd = needed.Except(existing).Select(n => new BrandEntity { Name = n }).ToList();
        if (toAdd.Any())
        {
            _dbContext.Brand.AddRange(toAdd);
            await _dbContext.SaveChangesAsync();
        }
    }

    private async Task SeedCpusAsync()
    {
        var brands = await _dbContext.Brand.ToDictionaryAsync(b => b.Name);
        var cpus = new List<CpuEntity>
        {
            new CpuEntity
            {
                BrandId = brands["AMD"].Id,
                Name = "Ryzen 7 9800",
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
                Name = "Core Ultra 9",
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
                Currency = Currency.UAH,
                Price = 325.99m
            }
        };
        _dbContext.Cpu.AddRange(cpus);
    }
}
