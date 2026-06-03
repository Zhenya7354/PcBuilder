using Microsoft.AspNetCore.Authentication;
using Microsoft.EntityFrameworkCore;
using PcBuilder.Data;
using PcBuilder.Data.Seeding;
using PcBuilder.Data.Seeding.Interfaces;
using PcBuilder.Data.Seeding.Seeders;
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
        builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
        builder.Services.AddScoped<IDataSeeder, BrandSeeder>();
        builder.Services.AddScoped<IDataSeeder, CpuCoolerSeeder>();
        builder.Services.AddScoped<IDataSeeder, CpuSeeder>();
        builder.Services.AddScoped<IDataSeeder, GpuSeeder>();
        builder.Services.AddScoped<IDataSeeder, HardDriveSeeder>();
        builder.Services.AddScoped<IDataSeeder, MotherboardSeeder>();
        builder.Services.AddScoped<IDataSeeder, PcCaseSeeder>();
        builder.Services.AddScoped<IDataSeeder, PcMonitorSeeder>();
        builder.Services.AddScoped<IDataSeeder, PsuSeeder>();
        builder.Services.AddScoped<IDataSeeder, RamSeeder>();
        builder.Services.AddScoped<IDbSeeder, DbSeeder>();
        builder.Services.AddScoped<IUserContextAccessor, UserContextAccessor>();

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
        builder.Services.AddHttpContextAccessor();

        builder.Services.AddDbContext<PcDbContext>(options =>
            options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

        builder.Services.ConfigureHttpJsonOptions(options =>
        {
            options.SerializerOptions.Converters.Add(new System.Text.Json.Serialization.JsonStringEnumConverter());
        });


        return builder;
    }
}
