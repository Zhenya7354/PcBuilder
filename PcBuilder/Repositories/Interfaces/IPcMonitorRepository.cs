using PcBuilder.Entities;

namespace PcBuilder.Repositories.Interfaces;

public interface IPcMonitorRepository
{
    public Task<List<PcMonitorEntity>> GetAllMonitorsAsync();
    public Task<PcMonitorEntity?> GetMonitorByIdAsync(int id);
    public Task AddMonitor(PcMonitorEntity monitor);
    public Task DeleteMonitor(PcMonitorEntity monitor);
    public Task<bool> BrandExistsAsync(int brandId);
    public Task SaveChangesAsync();
}
