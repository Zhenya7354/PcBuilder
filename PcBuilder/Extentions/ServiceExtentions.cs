using Microsoft.AspNetCore.Authentication;
using Microsoft.EntityFrameworkCore;
using PcBuilder.Data;
using PcBuilder.Enums;
using PcBuilder.Services;
using System.Text.Json.Serialization;

namespace PcBuilder.Extentions;

public static class ServiceExtentions
{
    public static WebApplicationBuilder AddAppServices(this WebApplicationBuilder builder)
    {
        builder.Services.AddScoped<JwtService>();
        builder.Services.AddScoped<CpuService>();
        builder.Services.AddScoped<GpuService>();
        builder.Services.AddScoped<CpuCoolerService>();
        builder.Services.AddScoped<MotherboardService>();
        builder.Services.AddScoped<RamService>();
        builder.Services.AddScoped<PsuService>();
        builder.Services.AddScoped<PcCaseService>();
        builder.Services.AddScoped<PcMonitorService>();
        builder.Services.AddScoped<HardDriveService>();
        builder.Services.AddScoped<BrandService>();
        builder.Services.AddScoped<BuildService>();
        builder.Services.AddScoped<CompatibilityCheckService>();
        builder.Services.AddOpenApi();
        builder.Services.AddDbContext<PcDbContext>(options =>
            options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection"), o =>
            {
                o.MapEnum<CoolerType>("cooler_type");
                o.MapEnum<FormFactor>("form_factor");
                o.MapEnum<GpuInterface>("gpu_interface");
                o.MapEnum<MemoryType>("memory_type");
                o.MapEnum<PanelType>("panel_type");
                o.MapEnum<PsuModular>("psu_modular");
                o.MapEnum<PsuRating>("psu_rating");
                o.MapEnum<PcSocketType>("socket_type");
                o.MapEnum<StorageFormFactor>("storage_form_factor");
                o.MapEnum<StorageInterface>("storage_interface");
            }));

        builder.Services.ConfigureHttpJsonOptions(options =>
        {
            options.SerializerOptions.Converters.Add(new System.Text.Json.Serialization.JsonStringEnumConverter());
        });


        return builder;
    }
}
