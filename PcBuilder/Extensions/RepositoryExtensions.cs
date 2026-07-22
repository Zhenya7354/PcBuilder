using PcBuilder.Repositories;
using PcBuilder.Repositories.Decorators;
using PcBuilder.Repositories.Interfaces;

namespace PcBuilder.Extensions;

public static class RepositoryExtensions
{
    public static WebApplicationBuilder AddRepositories(this WebApplicationBuilder builder)
    {
        AddCachedRepositories<ICpuRepository, CpuRepository, CachedCpuRepository>(builder.Services);
        AddCachedRepositories<IGpuRepository, GpuRepository, CachedGpuRepository>(builder.Services);
        AddCachedRepositories<ICpuCoolerRepository, CpuCoolerRepository, CachedCpuCoolerRepository>(builder.Services);
        AddCachedRepositories<IMotherboardRepository, MotherboardRepository, CachedMotherboardRepository>(builder.Services);
        AddCachedRepositories<IRamRepository, RamRepository, CachedRamRepository>(builder.Services);
        AddCachedRepositories<IPsuRepository, PsuRepository, CachedPsuRepository>(builder.Services);
        AddCachedRepositories<IPcCaseRepository, PcCaseRepository, CachedPcCaseRepository>(builder.Services);
        AddCachedRepositories<IPcMonitorRepository, PcMonitorRepository, CachedPcMonitorRepository>(builder.Services);
        AddCachedRepositories<IHardDriveRepository, HardDriveRepository, CachedHardDriveRepository>(builder.Services);
        builder.Services.AddScoped<IBrandRepository, BrandRepository>();
        builder.Services.AddScoped<IAdminRepository, AdminRepository>();
        builder.Services.AddScoped<IBuildRepository, BuildRepository>();
        builder.Services.AddScoped<ICompatibilityCheckRepository, CompatibilityCheckRepository>();
        builder.Services.AddScoped<IRegularUserRepository, RegularUserRepository>();

        return builder;
    }
    private static void AddCachedRepositories<TInterface, TImplementation,TDecorator>(
        IServiceCollection services)
        where TInterface : class
        where TImplementation : class, TInterface
        where TDecorator : class, TInterface
    {
        services.AddScoped<TImplementation>();
        services.AddScoped<TInterface, TDecorator>();
    }
}
