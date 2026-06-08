using PcBuilder.Entities;

namespace PcBuilder.Repositories.Interfaces;

public interface IPcMonitorRepository
{
    public Task<List<PcMonitorEntity>> GetAllMonitorsAsync(CancellationToken cancellationToken);
    public Task<PcMonitorEntity?> GetMonitorByIdAsync(int id, CancellationToken cancellationToken);
    public Task AddMonitorAsync(PcMonitorEntity monitor, CancellationToken cancellationToken);
    public Task DeleteMonitorAsync(PcMonitorEntity monitor, CancellationToken cancellationToken);
    public Task<bool> BrandExistsAsync(int brandId, CancellationToken cancellationToken);
    public Task SaveChangesAsync(CancellationToken cancellationToken);
}
