using Microsoft.AspNetCore.Authentication;
using Microsoft.EntityFrameworkCore;
using PcBuilder.Data;
using PcBuilder.Data.Seeding;
using PcBuilder.Data.Seeding.Interfaces;
using PcBuilder.Data.Seeding.Seeders;
using PcBuilder.Enums;
using PcBuilder.Exceptions;
using PcBuilder.Repositories;
using PcBuilder.Repositories.Interfaces;
using PcBuilder.Services;
using PcBuilder.Services.Interfaces;
using System.Text.Json.Serialization;

namespace PcBuilder.Extentions;

public static class ServiceExtensions
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
        builder.Services.AddScoped<IAuthService, AuthService>(); 
        builder.Services.AddScoped<IUserContextAccessor, UserContextAccessor>();

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
