using PcBuilder.Entities;

namespace PcBuilder.Repositories.Interfaces;

public interface IBuildRepository
{
    public Task<List<BuildEntity>> GetAllAsync(string userId);
    public Task<BuildEntity?> GetByIdAsync(int buildId, string userId);
    public Task AddBuildAsync(BuildEntity build);
    public Task DeleteBuildAsync(BuildEntity build);
    public Task SaveChangesAsync();
    public Task<bool> CpuExistsAsync(int cpuId);
    public Task<bool> CpuCoolerExistsAsync(int cpuCoolerId);
    public Task<bool> GpuExistsAsync(int gpuId);
    public Task<bool> RamExistsAsync(int ramId);
    public Task<bool> HardDriveExistsAsync(int hardDriveId);
    public Task<bool> MotherboardExistsAsync(int motherboardId);
    public Task<bool> PsuExistsAsync(int psuId);
    public Task<bool> CaseExistsAsync(int caseId);
    public Task<bool> MonitorExistsAsync(int monitorId);
}
