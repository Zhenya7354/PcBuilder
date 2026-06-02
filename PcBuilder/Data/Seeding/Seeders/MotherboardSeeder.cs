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
        if (await context.Motherboard.AnyAsync())
            return;

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
                PriceUsd = 399.99m
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
                PriceUsd = 179.99m
            }
        };

        await context.Motherboard.AddRangeAsync(motherboards);
        await context.SaveChangesAsync();
    }
}
