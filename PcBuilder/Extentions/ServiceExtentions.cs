using Microsoft.AspNetCore.Authentication;
using Microsoft.EntityFrameworkCore;
using PcBuilder.Data;
using PcBuilder.Enums;
using PcBuilder.Repositories;
using PcBuilder.Repositories.Interfaces;
using PcBuilder.Services;
using PcBuilder.Services.Interfaces;
using System.Text.Json.Serialization;

namespace PcBuilder.Extentions;

public static class ServiceExtentions
{
    public static WebApplicationBuilder AddAppServices(this WebApplicationBuilder builder)
    {
        builder.Services.AddScoped<IJwtService, JwtService>();
        builder.Services.AddScoped<ICpuService, CpuService>();
        builder.Services.AddScoped<IGpuService, GpuService>();
        builder.Services.AddScoped<ICpuCoolerService, CpuCoolerService>();
        builder.Services.AddScoped<IMotherboardService, MotherboardService>();
        builder.Services.AddScoped<IRamService, RamService>();
        builder.Services.AddScoped<IPsuService, PsuService>();
        builder.Services.AddScoped<IPcCaseService, PcCaseService>();
        builder.Services.AddScoped<IPcMonitorService, PcMonitorService>();
        builder.Services.AddScoped<IHardDriveService, HardDriveService>();
        builder.Services.AddScoped<IBrandService, BrandService>();
        builder.Services.AddScoped<IBuildService, BuildService>();
        builder.Services.AddScoped<ICompatibilityCheckService, CompatibilityCheckService>();
        builder.Services.AddScoped<IAdminService, AdminService>();
        builder.Services.AddScoped<ITransactionService, TransactionService>();
        builder.Services.AddScoped<ICpuRepository, CpuRepository>();
        builder.Services.AddScoped<IGpuRepository, GpuRepository>();
        builder.Services.AddScoped<IBrandRepository, BrandRepository>();
        builder.Services.AddScoped<ICpuCoolerRepository, CpuCoolerRepository>();
        builder.Services.AddScoped<IMotherboardRepository, MotherboardRepository>();
        builder.Services.AddScoped<IRamRepository, RamRepository>();
        builder.Services.AddScoped<IPsuRepository, PsuRepository>();
        builder.Services.AddScoped<IPcCaseRepository, PcCaseRepository>();
        builder.Services.AddScoped<IPcMonitorRepository, PcMonitorRepository>();
        builder.Services.AddScoped<IHardDriveRepository, HardDriveRepository>();
        builder.Services.AddScoped<IAdminRepository, AdminRepository>();
        builder.Services.AddScoped<IBuildRepository, BuildRepository>();
        builder.Services.AddScoped<ICompatibilityCheckRepository, CompatibilityCheckRepository>();
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
