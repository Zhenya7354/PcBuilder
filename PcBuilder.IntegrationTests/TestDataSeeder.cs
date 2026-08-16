using Microsoft.EntityFrameworkCore;
using PcBuilder.Data;
using PcBuilder.Entities;
using PcBuilder.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace PcBuilder.IntegrationTests;

public class TestDataSeeder(PcDbContext dbContext)
{
    public async Task SeedAsync()
    {
        await SeedBrandsAsync();
        await SeedAiBuildComponentsAsync();
        await dbContext.SaveChangesAsync();
    }

    private async Task SeedBrandsAsync()
    {
        var needed = new[] { "AMD", "Intel" };
        var existing = await dbContext.Brand.Where(b => needed.Contains(b.Name)).Select(b => b.Name).ToListAsync();
        var toAdd = needed.Except(existing).Select(n => new BrandEntity { Name = n }).ToList();
        if (toAdd.Any())
        {
            dbContext.Brand.AddRange(toAdd);
            await dbContext.SaveChangesAsync();
        }
    }

    
    private async Task SeedAiBuildComponentsAsync()
    {
        var brands = await dbContext.Brand.ToDictionaryAsync(b => b.Name);

        var existingCpus = await dbContext.Cpu.Select(c => c.Name).ToListAsync();
        var cpus = new List<CpuEntity>
        {
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
                LaunchedYear = 2020,
                Currency = Currency.USD,
                Price = 220.00m
            }
        };
        var newCpus = cpus.Where(c => !existingCpus.Contains(c.Name)).ToList();
        if (newCpus.Any()) dbContext.Cpu.AddRange(newCpus);

        var existingGpus = await dbContext.Gpu.Select(g => g.Name).ToListAsync();
        var gpus = new List<GpuEntity>
        {
            new GpuEntity
            {
                BrandId = brands["Intel"].Id,
                Name = "GTX 1660 Super",
                Price = 420.00m,
                GpuChip = "GTX 1660 Super",
                VramGb = 6,
                TdpWatts = 125,
                RecommendedPsuWattage = 450,
                CardLengthMm = 230
            }
        };
        var newGpus = gpus.Where(g => !existingGpus.Contains(g.Name)).ToList();
        if (newGpus.Any()) dbContext.Gpu.AddRange(newGpus);

        var existingMbs = await dbContext.Motherboard.Select(m => m.Name).ToListAsync();
        var mbs = new List<MotherboardEntity>
        {
            new MotherboardEntity
            {
                BrandId = brands["AMD"].Id,
                Name = "B550 Micro-ATX",
                Price = 110.00m,
                Socket = PcSocketType.AM4,
                FormFactor = FormFactor.MicroATX,
                MemoryType = MemoryType.DDR4,
                MemorySlots = 4,
                M2Slots = 1,
                Chipset = "B550",
                MaxMemorySpeedMhz = 4000,
                MaxMemoryGb = 64
            }
        };
        var newMbs = mbs.Where(m => !existingMbs.Contains(m.Name)).ToList();
        if (newMbs.Any()) dbContext.Motherboard.AddRange(newMbs);

        var existingRams = await dbContext.Ram.Select(r => r.Name).ToListAsync();
        var rams = new List<RamEntity>
        {
            new RamEntity
            {
                BrandId = brands["AMD"].Id,
                Name = "Corsair Vengeance 16GB (2x8)",
                Price = 80.00m,
                MemoryType = MemoryType.DDR4,
                CapacityGb = 16,
                KitCount = 2,
                SpeedMhz = 3200
            }
        };
        var newRams = rams.Where(r => !existingRams.Contains(r.Name)).ToList();
        if (newRams.Any()) dbContext.Ram.AddRange(newRams);

        var existingDrives = await dbContext.HardDrive.Select(d => d.Name).ToListAsync();
        var drives = new List<HardDriveEntity>
        {
            new HardDriveEntity
            {
                BrandId = brands["Intel"].Id,
                Name = "WD Blue SN550 1TB",
                Price = 70.00m,
                CapacityGb = 1000,
                DriveInterface = StorageInterface.NvmePcie_Gen3,
                FormFactor = StorageFormFactor.M2_2260,
                PcDriveType = PcDriveType.SSD
            }
        };
        var newDrives = drives.Where(d => !existingDrives.Contains(d.Name)).ToList();
        if (newDrives.Any()) dbContext.HardDrive.AddRange(newDrives);

        var existingPsu = await dbContext.Psu.Select(p => p.Name).ToListAsync();
        var psus = new List<PsuEntity>
        {
            new PsuEntity
            {
                BrandId = brands["Intel"].Id,
                Name = "EVGA 500W",
                Price = 60.00m,
                Wattage = 500,
                Efficiency = PsuRating.Bronze,
                EpsConnectors = 1,
                Pcie8PinConnectors = 2
            }
        };
        var newPsus = psus.Where(p => !existingPsu.Contains(p.Name)).ToList();
        if (newPsus.Any()) dbContext.Psu.AddRange(newPsus);

        var existingCases = await dbContext.PcCase.Select(c => c.Name).ToListAsync();
        var cases = new List<PcCaseEntity>
        {
            new PcCaseEntity
            {
                BrandId = brands["AMD"].Id,
                Name = "Mini Tower",
                Price = 30.00m,
                SupportedFormFactors = new List<FormFactor> { FormFactor.MicroATX, FormFactor.MiniITX },
                MaxGpuLengthMm = 260,
                IncludedFans = 1
            }
        };
        var newCases = cases.Where(c => !existingCases.Contains(c.Name)).ToList();
        if (newCases.Any()) dbContext.PcCase.AddRange(newCases);
        var existingCoolers = await dbContext.CpuCooler.Select(cc => cc.Name).ToListAsync();
        var coolers = new List<CpuCoolerEntity>
        {
            new CpuCoolerEntity
            {
                BrandId = brands["AMD"].Id,
                Name = "Budget Air Cooler",
                Price = 10.00m,
                CoolerType = CoolerType.Air,
                SocketsSupported = new List<PcSocketType> { PcSocketType.AM4 },
                MaxTdpWatts = 95,
                FanCount = 1,
                FanSizeMm = 92
            }
        };
        var newCoolers = coolers.Where(cc => !existingCoolers.Contains(cc.Name)).ToList();
        if (newCoolers.Any()) dbContext.CpuCooler.AddRange(newCoolers);
        
    }
}
