using Microsoft.EntityFrameworkCore;
using PcBuilder.Data.Seeding.Interfaces;
using PcBuilder.Entities;
using PcBuilder.Enums;

namespace PcBuilder.Data.Seeding.Seeders;

public class HardDriveSeeder : IDataSeeder
{
    public async Task SeedAsync(PcDbContext context)
    {
        if (await context.HardDrive.AnyAsync())
            return;

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
                IsSsd = false,
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
                IsSsd = false,
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
                IsSsd = true,
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
                IsSsd = true,
                PowerWatts = 15,
                Currency = Currency.USD,
                Price = 100m,
                ReadSpeedMbS = 220,
                WriteSpeedMbs = 150,
                Rpm = 7200,
                Tbw = 600
            }
        };
        await context.HardDrive.AddRangeAsync(hardDrives);
        await context.SaveChangesAsync();
    }
}
