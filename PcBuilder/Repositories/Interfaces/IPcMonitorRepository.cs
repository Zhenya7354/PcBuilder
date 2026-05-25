using PcBuilder.Entities;

namespace PcBuilder.Repositories.Interfaces;

public interface IPcMonitorRepository
{
    public Task<List<PcMonitorEntity>> GetAllMonitorsAsync();
    public Task<PcMonitorEntity?> GetMonitorByIdAsync(int id);
    public Task AddMonitorAsync(PcMonitorEntity monitor);
    public Task DeleteMonitorAsync(PcMonitorEntity monitor);
    public Task<bool> BrandExistsAsync(int brandId);
    public Task SaveChangesAsync();
}
