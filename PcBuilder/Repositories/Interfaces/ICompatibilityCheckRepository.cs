using PcBuilder.Entities;

namespace PcBuilder.Repositories.Interfaces;

public interface ICompatibilityCheckRepository
{
    public Task<CpuEntity?> GetCpuByIdAsync(int cpuId, CancellationToken cancellationToken);
    public Task<GpuEntity?> GetGpuByIdAsync(int gpuId, CancellationToken cancellationToken);
    public Task<PcMonitorEntity?> GetMonitorByIdAsync(int monitorId, CancellationToken cancellationToken);
    public Task<PcCaseEntity?> GetCaseByIdAsync(int caseId, CancellationToken cancellationToken);
    public Task<RamEntity?> GetRamByIdAsync(int ramId, CancellationToken cancellationToken);
    public Task<PsuEntity?> GetPsuByIdAsync(int psuId, CancellationToken cancellationToken);
    public Task<CpuCoolerEntity?> GetCpuCoolerByIdAsync(int cpuCoolerId, CancellationToken cancellationToken);
    public Task<MotherboardEntity?> GetMotherboardByIdAsync(int motherboardId, CancellationToken cancellationToken);
    public Task<HardDriveEntity?> GetHardDriveByIdAsync(int hardDriveId, CancellationToken cancellationToken);
}
