using Microsoft.EntityFrameworkCore;
using PcBuilder.Data.Seeding.Interfaces;
using PcBuilder.Entities;
using PcBuilder.Enums;

namespace PcBuilder.Data.Seeding.Seeders;

public class PcMonitorSeeder : IDataSeeder
{
    public async Task SeedAsync(PcDbContext context)
    {
        if (await context.PcMonitor.AnyAsync())
            return;

        var brands = await context.Brand.ToDictionaryAsync(b => b.Name);

        var pcMonitors = new List<PcMonitorEntity>()
        {
            new PcMonitorEntity
            {
                Name = "Dell UltraSharp U2723QE",
                Brand = brands["Dell"],
                ScreenSizeInch = 27,
                ResolutionWidth = 3840,
                ResolutionHeight = 2160,
                PanelType = PanelType.IPS,
                RefreshRateHz = 60,
                ResponseTimeMs = 5,
                HdrSupport = "HDR400",
                BrightnessNits = 400,
                ContrastRatio = "1000:1",
                ColorGamutP3 = 98,
                SyncTechnologies = new List<SyncTechnology> { SyncTechnology.GSync, SyncTechnology.FreeSync },
                HdmiPorts = 2,
                HdmiVersion = "2.0",
                DpPorts = 1,
                DpVersion = "1.4",
                UsbCPorts = 2,
                HasUsbHub = true,
                HasSpeakers = false,
                HeightAdjustable = true,
                VesaMount = "100x100",
                Currency = Currency.USD,
                Price = 699.99m 
            },
            new PcMonitorEntity
            {
                Name = "ASUS ROG Swift PG32UQX",
                Brand = brands["ASUS"],
                ScreenSizeInch = 32,
                ResolutionWidth = 3840,
                ResolutionHeight = 2160,
                PanelType = PanelType.IPS,
                RefreshRateHz = 144,
                ResponseTimeMs = 1,
                HdrSupport = "HDR1400",
                BrightnessNits = 1400,
                ContrastRatio = "1000:1",
                ColorGamutP3 = 98,
                SyncTechnologies = new List<SyncTechnology> { SyncTechnology.GSync, SyncTechnology.FreeSyncPremium },
                HdmiPorts = 2,
                HdmiVersion = "2.1",
                DpPorts = 1,
                DpVersion = "1.4",
                UsbCPorts = 2,
                HasUsbHub = true,
                HasSpeakers = true,
                HeightAdjustable = true,
                VesaMount = "100x100",
                Currency = Currency.USD,
                Price = 2999.99m
            }
        };
        await context.PcMonitor.AddRangeAsync(pcMonitors);
        await context.SaveChangesAsync();
    }
}
