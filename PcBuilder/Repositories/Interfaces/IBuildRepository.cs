using PcBuilder.Entities;

namespace PcBuilder.Repositories.Interfaces;

public interface IBuildRepository
{
    public Task<List<BuildEntity>> GetAllAsync(string userId, CancellationToken cancellationToken);
    public Task<BuildEntity?> GetByIdAsync(int buildId, string userId, CancellationToken cancellationToken);
    public Task AddBuildAsync(BuildEntity build, CancellationToken cancellationToken);
    public Task DeleteBuildAsync(BuildEntity build, CancellationToken cancellationToken);
    public Task SaveChangesAsync(CancellationToken cancellationToken);
    public Task<bool> CpuExistsAsync(int cpuId, CancellationToken cancellationToken);
    public Task<bool> CpuCoolerExistsAsync(int cpuCoolerId, CancellationToken cancellationToken);
    public Task<bool> GpuExistsAsync(int gpuId, CancellationToken cancellationToken);
    public Task<bool> RamExistsAsync(int ramId, CancellationToken cancellationToken);
    public Task<bool> HardDriveExistsAsync(int hardDriveId, CancellationToken cancellationToken);
    public Task<bool> MotherboardExistsAsync(int motherboardId, CancellationToken cancellationToken);
    public Task<bool> PsuExistsAsync(int psuId, CancellationToken cancellationToken);
    public Task<bool> CaseExistsAsync(int caseId, CancellationToken cancellationToken);
    public Task<bool> MonitorExistsAsync(int monitorId, CancellationToken cancellationToken);
}
