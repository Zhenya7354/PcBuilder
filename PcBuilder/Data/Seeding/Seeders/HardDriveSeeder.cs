using Microsoft.EntityFrameworkCore;
using PcBuilder.Data.Seeding.Interfaces;
using PcBuilder.Entities;
using PcBuilder.Enums;

namespace PcBuilder.Data.Seeding.Seeders;

public class HardDriveSeeder : IDataSeeder
{
    public async Task SeedAsync(PcDbContext context)
    {
        var brands = await context.Brand.ToDictionaryAsync(b => b.Name);

        var hardDrives = new List<HardDriveEntity>
        {
            new HardDriveEntity
            {
                Name = "Seagate BarraCuda 2TB",
                BrandId = brands["Seagate"].Id,
                CacheMb = 256,
                CapacityGb = 2000,
                DriveInterface = Enums.StorageInterface.NvmePcie_Gen3,
                FormFactor = Enums.StorageFormFactor.Sata_2_5,
                PcDriveType = Enums.PcDriveType.HDD,
                PowerWatts = 15,
                Currency = Currency.USD,
                Price = 100m,
                ReadSpeedMbS = 220,
                WriteSpeedMbs = 150,
                Rpm = 7200,
                Tbw = 600
            },
            new HardDriveEntity
            {
                Name = "Western Digital Blue 1TB",
                BrandId = brands["Western Digital"].Id,
                CacheMb = 256,
                CapacityGb = 2000,
                DriveInterface = Enums.StorageInterface.Sata_3,
                FormFactor = Enums.StorageFormFactor.M2_22110,
                PcDriveType = Enums.PcDriveType.HDD,
                PowerWatts = 15,
                Currency = Currency.USD,
                Price = 100m,
                ReadSpeedMbS = 220,
                WriteSpeedMbs = 150,
                Rpm = 7200,
                Tbw = 600
            },
            new HardDriveEntity
            {
                Name = "Samsung 970 EVO Plus 500GB",
                BrandId = brands["Samsung"].Id,
                CacheMb = 256,
                CapacityGb = 2000,
                DriveInterface = Enums.StorageInterface.SAS,
                FormFactor = Enums.StorageFormFactor.AddInCard,
                PcDriveType = Enums.PcDriveType.Nvme,
                PowerWatts = 15,
                Currency = Currency.USD,
                Price = 100m,
                ReadSpeedMbS = 220,
                WriteSpeedMbs = 150,
                Rpm = 7200,
                Tbw = 600
            },
            new HardDriveEntity
            {
                Name = "Crucial MX500 1TB",
                BrandId = brands["Crucial"].Id,
                CacheMb = 256,
                CapacityGb = 2000,
                DriveInterface = Enums.StorageInterface.NvmePcie_Gen4,
                FormFactor = Enums.StorageFormFactor.Sata_3_5,
                PcDriveType = Enums.PcDriveType.Nvme,
                PowerWatts = 15,
                Currency = Currency.USD,
                Price = 100m,
                ReadSpeedMbS = 220,
                WriteSpeedMbs = 150,
                Rpm = 7200,
                Tbw = 600
            },
            new HardDriveEntity
            {
                Name = "Crucial P3 Plus 1TB",
                BrandId = brands["Crucial"].Id,
                CacheMb = 256,
                CapacityGb = 1000,
                DriveInterface = Enums.StorageInterface.NvmePcie_Gen4,
                FormFactor = Enums.StorageFormFactor.M2_2280,
                PcDriveType = Enums.PcDriveType.Nvme,
                PowerWatts = 8,
                Currency = Currency.USD,
                Price = 59.99m,
                ReadSpeedMbS = 5000,
                WriteSpeedMbs = 3600,
                Rpm = null,
                Tbw = 220
            },
            new HardDriveEntity
            {
                Name = "Samsung 990 EVO Plus 2TB",
                BrandId = brands["Samsung"].Id,
                CacheMb = 2048,
                CapacityGb = 2000,
                DriveInterface = Enums.StorageInterface.NvmePcie_Gen4,
                FormFactor = Enums.StorageFormFactor.M2_2280,
                PcDriveType = Enums.PcDriveType.Nvme,
                PowerWatts = 9,
                Currency = Currency.USD,
                Price = 149.99m,
                ReadSpeedMbS = 7250,
                WriteSpeedMbs = 6300,
                Rpm = null,
                Tbw = 1200
            }
        };
        var existingNames = await context.HardDrive.Select(h => h.Name).ToListAsync();
        var newHardDrives = hardDrives
            .Where(h => !existingNames.Contains(h.Name))
            .ToList();

        if (newHardDrives.Count == 0)
            return;

        await context.HardDrive.AddRangeAsync(newHardDrives);
        await context.SaveChangesAsync();
    }
}
