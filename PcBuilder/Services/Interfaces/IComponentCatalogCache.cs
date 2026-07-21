using PcBuilder.Entities;
using PcBuilder.Enums;
using PcBuilder.Models;

namespace PcBuilder.Services.Interfaces;

public interface IComponentCatalogCache
{
    Task<List<CpuEntity>> GetAllCpusAsync(CancellationToken cancellationToken);
    Task<List<GpuEntity>> GetAllGpusAsync(CancellationToken cancellationToken);
    Task<List<RamEntity>> GetAllRamsAsync(CancellationToken cancellationToken);
    Task<List<HardDriveEntity>> GetAllHardDrivesAsync(CancellationToken cancellationToken);
    Task<List<MotherboardEntity>> GetAllMotherboardsAsync(CancellationToken cancellationToken);
    Task<List<PsuEntity>> GetAllPsusAsync(CancellationToken cancellationToken);
    Task<List<PcCaseEntity>> GetAllCasesAsync(CancellationToken cancellationToken);
    Task<List<PcMonitorEntity>> GetAllMonitorsAsync(CancellationToken cancellationToken);
    Task<List<CpuCoolerEntity>> GetAllCpuCoolersAsync(CancellationToken cancellationToken);

    void InvalidateCache(BuildComponentType componentType);
    void InvalidateAllCaches();
}
