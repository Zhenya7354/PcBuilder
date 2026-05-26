using PcBuilder.Entities;

namespace PcBuilder.Repositories.Interfaces;

public interface ICompatibilityCheckRepository
{
    public Task<CpuEntity?> GetCpuByIdAsync(int cpuId);
    public Task<GpuEntity?> GetGpuByIdAsync(int gpuId);
    public Task<PcMonitorEntity?> GetMonitorByIdAsync(int monitorId);
    public Task<PcCaseEntity?> GetCaseByIdAsync(int caseId);
    public Task<RamEntity?> GetRamByIdAsync(int ramId);
    public Task<PsuEntity?> GetPsuByIdAsync(int psuId);
    public Task<CpuCoolerEntity?> GetCpuCoolerByIdAsync(int cpuCoolerId);
    public Task<MotherboardEntity?> GetMotherboardByIdAsync(int motherboardId);
    public Task<HardDriveEntity?> GetHardDriveByIdAsync(int hardDriveId);
}
