using Microsoft.EntityFrameworkCore;
using PcBuilder.Data.Seeding.Interfaces;
using PcBuilder.Entities;
using PcBuilder.Enums;

namespace PcBuilder.Data.Seeding.Seeders;

public class PcCaseSeeder : IDataSeeder
{
    public async Task SeedAsync(PcDbContext context)
    {
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
                ColorScheme = ColorScheme.ARGB,
                Currency = Currency.USD,
                Price = 79.99m,
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
                ColorScheme = ColorScheme.ARGB,
                Currency = Currency.USD,
                Price = 94.99m,
                BrandId = brands["Corsair"].Id
            },
            new PcCaseEntity
            {
                Name = "Cooler Master MasterBox Q300L",
                SupportedFormFactors = new List<FormFactor> { FormFactor.MicroATX, FormFactor.MiniITX },
                MaxGpuLengthMm = 360,
                MaxCpuCoolerHeightMm = 159,
                MaxPsuLengthMm = 160,
                DriveBays35Inch = 1,
                DriveBays25Inch = 2,
                FrontUsbA = 2,
                FrontUsbC = 0,
                RadiatorSupportMm = new List<string> { "120mm", "240mm" },
                CaseWidthMm = 230,
                CaseHeightMm = 387,
                CaseDepthMm = 378,
                HasGlassPanel = true,
                IncludedFans = 1,
                ColorScheme = ColorScheme.NonRGB,
                Currency = Currency.USD,
                Price = 59.99m,
                BrandId = brands["Cooler Master"].Id
            },
            new PcCaseEntity
            {
                Name = "NZXT H5 Flow",
                SupportedFormFactors = new List<FormFactor> { FormFactor.EATX, FormFactor.MicroATX, FormFactor.MiniITX },
                MaxGpuLengthMm = 365,
                MaxCpuCoolerHeightMm = 165,
                MaxPsuLengthMm = 200,
                DriveBays35Inch = 1,
                DriveBays25Inch = 2,
                FrontUsbA = 1,
                FrontUsbC = 1,
                RadiatorSupportMm = new List<string> { "120mm", "240mm", "280mm" },
                CaseWidthMm = 227,
                CaseHeightMm = 464,
                CaseDepthMm = 446,
                HasGlassPanel = true,
                IncludedFans = 2,
                ColorScheme = ColorScheme.NonRGB,
                Currency = Currency.USD,
                Price = 94.99m,
                BrandId = brands["NZXT"].Id
            },
            // Budget1000 Value kit — MicroATX; clears kit cooler/GPU/PSU sizes
            new PcCaseEntity
            {
                Name = "Budget1000 Value MicroATX Case",
                SupportedFormFactors = new List<FormFactor> { FormFactor.MicroATX, FormFactor.MiniITX },
                MaxGpuLengthMm = 350,
                MaxCpuCoolerHeightMm = 160,
                MaxPsuLengthMm = 160,
                DriveBays35Inch = 2,
                DriveBays25Inch = 2,
                FrontUsbA = 2,
                FrontUsbC = 1,
                RadiatorSupportMm = new List<string> { "120", "240" },
                CaseWidthMm = 210,
                CaseHeightMm = 420,
                CaseDepthMm = 400,
                HasGlassPanel = true,
                IncludedFans = 2,
                ColorScheme = ColorScheme.NonRGB,
                Currency = Currency.USD,
                Price = 64.99m,
                BrandId = brands["Cooler Master"].Id
            },
            // Budget2000 Performance kit — roomier clearances for higher-tier parts
            new PcCaseEntity
            {
                Name = "Budget2000 Performance Mid Tower",
                SupportedFormFactors = new List<FormFactor> { FormFactor.EATX, FormFactor.MicroATX, FormFactor.MiniITX },
                MaxGpuLengthMm = 400,
                MaxCpuCoolerHeightMm = 175,
                MaxPsuLengthMm = 200,
                DriveBays35Inch = 2,
                DriveBays25Inch = 3,
                FrontUsbA = 2,
                FrontUsbC = 1,
                RadiatorSupportMm = new List<string> { "120", "240", "280", "360" },
                CaseWidthMm = 230,
                CaseHeightMm = 480,
                CaseDepthMm = 460,
                HasGlassPanel = true,
                IncludedFans = 3,
                ColorScheme = ColorScheme.ARGB,
                Currency = Currency.USD,
                Price = 119.99m,
                BrandId = brands["NZXT"].Id
            }
        };
        var existingNames = await context.PcCase.Select(c => c.Name).ToListAsync();
        var newPcCases = PcCases
            .Where(c => !existingNames.Contains(c.Name))
            .ToList();

        if (newPcCases.Count == 0)
            return;

        await context.PcCase.AddRangeAsync(newPcCases);
        await context.SaveChangesAsync();
    }
}
