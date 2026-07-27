using Microsoft.EntityFrameworkCore;
using PcBuilder.Data.Seeding.Interfaces;
using PcBuilder.Entities;
using PcBuilder.Enums;
using System.Diagnostics;

namespace PcBuilder.Data.Seeding.Seeders;

public class MotherboardSeeder : IDataSeeder
{
    public async Task SeedAsync(PcDbContext context)
    {
        var brands = await context.Brand.ToDictionaryAsync(b => b.Name);

        var motherboards = new List<MotherboardEntity>()
        {
            new MotherboardEntity
            {
                Name = "ASUS ROG Strix Z790-E Gaming WiFi",
                BrandId = brands["ASUS"].Id,
                Socket = PcSocketType.LGA1700,
                Chipset = "Z790",
                FormFactor = FormFactor.MicroATX,
                MemoryType = MemoryType.DDR5,
                MemorySlots = 4,
                MaxMemoryGb = 128,
                MaxMemorySpeedMhz = 7200,
                PcieX16Slots = 2,
                PcieX1Slots = 3,
                M2Slots = 4,
                SataPorts = 6,
                UsbHeaders3Gen2 = 2,
                UsbHeaders2Gen0 = 1,
                HasWifi = true,
                HasBluetooth = true,
                LanSpeedGbps = 2,
                FanHeaders = 4,
                ArgbHeaders = 3,
                VrmPhases = 18,
                RearUsbA = 8,
                RearUsbC = 3,
                RearHdmi = false,
                RearDisplayPort = true,
                Currency = Currency.USD,
                Price = 399.99m
            },
            new MotherboardEntity
            {
                Name = "MSI MAG B550 Tomahawk",
                BrandId = brands["MSI"].Id,
                Socket = PcSocketType.AM4,
                Chipset = "B550",
                FormFactor = FormFactor.MicroATX,
                MemoryType = MemoryType.DDR4,
                MemorySlots = 4,
                MaxMemoryGb = 128,
                MaxMemorySpeedMhz = 4400,
                PcieX16Slots = 2,
                PcieX1Slots = 2,
                M2Slots = 2,
                SataPorts = 6,
                UsbHeaders3Gen2 = 1,
                UsbHeaders2Gen0 = 1,
                HasWifi = false,
                HasBluetooth = false,
                LanSpeedGbps = 1,
                FanHeaders = 4,
                ArgbHeaders = 2,
                VrmPhases = 14,
                RearUsbA = 6,
                RearUsbC = 1,
                RearHdmi = true,
                RearDisplayPort = true,
                Currency = Currency.USD,
                Price = 179.99m
            },
            new MotherboardEntity
            {
                Name = "Gigabyte B550M DS3H AC",
                BrandId = brands["Gigabyte"].Id,
                Socket = PcSocketType.AM4,
                Chipset = "B550",
                FormFactor = FormFactor.MicroATX,
                MemoryType = MemoryType.DDR4,
                MemorySlots = 4,
                MaxMemoryGb = 128,
                MaxMemorySpeedMhz = 3600,
                PcieX16Slots = 1,
                PcieX1Slots = 1,
                M2Slots = 2,
                SataPorts = 4,
                UsbHeaders3Gen2 = 1,
                UsbHeaders2Gen0 = 1,
                HasWifi = true,
                HasBluetooth = true,
                LanSpeedGbps = 1,
                FanHeaders = 3,
                ArgbHeaders = 1,
                VrmPhases = 8,
                RearUsbA = 6,
                RearUsbC = 0,
                RearHdmi = true,
                RearDisplayPort = true,
                Currency = Currency.USD,
                Price = 109.99m
            },
            new MotherboardEntity
            {
                Name = "MSI MAG B650M Mortar WiFi",
                BrandId = brands["MSI"].Id,
                Socket = PcSocketType.AM5,
                Chipset = "B650",
                FormFactor = FormFactor.MicroATX,
                MemoryType = MemoryType.DDR5,
                MemorySlots = 4,
                MaxMemoryGb = 192,
                MaxMemorySpeedMhz = 6400,
                PcieX16Slots = 2,
                PcieX1Slots = 1,
                M2Slots = 2,
                SataPorts = 6,
                UsbHeaders3Gen2 = 2,
                UsbHeaders2Gen0 = 1,
                HasWifi = true,
                HasBluetooth = true,
                LanSpeedGbps = 2,
                FanHeaders = 5,
                ArgbHeaders = 2,
                VrmPhases = 12,
                RearUsbA = 7,
                RearUsbC = 1,
                RearHdmi = true,
                RearDisplayPort = true,
                Currency = Currency.USD,
                Price = 219.99m
            }
        };

        var existingNames = await context.Motherboard.Select(m => m.Name).ToListAsync();
        var newMotherboards = motherboards
            .Where(m => !existingNames.Contains(m.Name))
            .ToList();

        if (newMotherboards.Count == 0)
            return;

        await context.Motherboard.AddRangeAsync(newMotherboards);
        await context.SaveChangesAsync();
    }
}
