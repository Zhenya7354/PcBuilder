using Microsoft.EntityFrameworkCore;
using PcBuilder.Data.Seeding.Interfaces;
using PcBuilder.Entities;
using PcBuilder.Enums;

namespace PcBuilder.Data.Seeding.Seeders;

public class PcCaseSeeder : IDataSeeder
{
    public async Task SeedAsync(PcDbContext context)
    {
        if (await context.PcCase.AnyAsync())
            return;

        var brands = await context.Brand.ToDictionaryAsync(b => b.Name);

        var PcCases = new List<PcCaseEntity>()
        {
            new PcCaseEntity
            {
                Name = "NZXT H510",
                SupportedFormFactors = new List<FormFactor> { FormFactor.EATX, FormFactor.MicroATX, FormFactor.MiniITX },
                MaxGpuLengthMm = 381,
                MaxCpuCoolerHeightMm = 165,
                MaxPsuLengthMm = 210,
                DriveBays35Inch = 2,
                DriveBays25Inch = 3,
                FrontUsbA = 2,
                FrontUsbC = 1,
                RadiatorSupportMm = new List<string> { "120mm", "240mm" },
                CaseWidthMm = 210,
                CaseHeightMm = 460,
                CaseDepthMm = 428,
                HasGlassPanel = true,
                IncludedFans = 2,
                PriceUsd = 79.99m,
                BrandId = brands["NZXT"].Id
            },
            new PcCaseEntity
            {
             Name = "Corsair 4000D Airflow",
                SupportedFormFactors = new List<FormFactor> { FormFactor.XLATX, FormFactor.MiniITX },
                MaxGpuLengthMm = 360,
                MaxCpuCoolerHeightMm = 170,
                MaxPsuLengthMm = 180,
                DriveBays35Inch = 2,
                DriveBays25Inch = 3,
                FrontUsbA = 2,
                FrontUsbC = 1,
                RadiatorSupportMm = new List<string> { "120mm", "240mm", "360mm" },
                CaseWidthMm = 230,
                CaseHeightMm = 466,
                CaseDepthMm = 453,
                HasGlassPanel = true,
                IncludedFans = 2,
                PriceUsd = 94.99m,
                BrandId = brands["Corsair"].Id
            }
        };
        await context.PcCase.AddRangeAsync(PcCases);
        await context.SaveChangesAsync();
    }
}
