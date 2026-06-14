using PcBuilder.Repositories;
using PcBuilder.Repositories.Interfaces;

namespace PcBuilder.Extensions;

public static class RepositoryExtensions
{
    public static WebApplicationBuilder AddRepositories(this WebApplicationBuilder builder)
    {
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
        builder.Services.AddScoped<IRegularUserRepository, RegularUserRepository>();

        return builder;
    }
}
