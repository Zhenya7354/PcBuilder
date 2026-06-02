using Microsoft.EntityFrameworkCore;
using PcBuilder.Data.Seeding.Interfaces;
using PcBuilder.Entities;

namespace PcBuilder.Data.Seeding.Seeders;

public class BrandSeeder : IDataSeeder
{
    public async Task SeedAsync(PcDbContext context)
    {
        if (await context.Brand.AnyAsync())
            return;
        
        var brands = new List<BrandEntity>
        {
            new BrandEntity { Name = "ASUS" },
            new BrandEntity { Name = "MSI" },
            new BrandEntity { Name = "Gigabyte" },
            new BrandEntity { Name = "ASRock" },
            new BrandEntity { Name = "EVGA" },
            new BrandEntity { Name = "Zotac" },
            new BrandEntity { Name = "Sapphire" },
            new BrandEntity { Name = "XFX" },
            new BrandEntity { Name = "PowerColor" },
            new BrandEntity { Name = "Inno3D" },
            new BrandEntity { Name = "AMD" },
            new BrandEntity { Name = "Intel" },
            new BrandEntity { Name = "Cooler Master" },
            new BrandEntity { Name = "Thermalright" },
            new BrandEntity { Name = "Seagate" },
            new BrandEntity { Name = "Western Digital" },
            new BrandEntity { Name = "Samsung" },
            new BrandEntity { Name = "Crucial" },
            new BrandEntity { Name = "NVIDIA" },
            new BrandEntity { Name = "NZXT" },
            new BrandEntity { Name = "Corsair" },
            new BrandEntity { Name = "Dell" },
            new BrandEntity { Name = "Seasonic" },
            new BrandEntity { Name = "Kingston" },
            new BrandEntity { Name = "G.Skill" }
        };
        
        await context.Brand.AddRangeAsync(brands);
        await context.SaveChangesAsync();
    }
}
