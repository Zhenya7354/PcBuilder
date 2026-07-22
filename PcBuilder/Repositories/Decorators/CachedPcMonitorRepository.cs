using PcBuilder.Entities;
using PcBuilder.Models;
using PcBuilder.Repositories.Interfaces;
using PcBuilder.Services.Interfaces;

namespace PcBuilder.Repositories.Decorators;

public class CachedPcMonitorRepository(
    PcMonitorRepository inner,
    IMemoryListCache cache) : IPcMonitorRepository
{
    public Task AddMonitorAsync(PcMonitorEntity monitor, CancellationToken cancellationToken)=>
        inner.AddMonitorAsync(monitor, cancellationToken);

    public Task<bool> BrandExistsAsync(int brandId, CancellationToken cancellationToken)=>
        inner.BrandExistsAsync(brandId, cancellationToken);

    public Task DeleteMonitorAsync(PcMonitorEntity monitor, CancellationToken cancellationToken)=>
        inner.DeleteMonitorAsync(monitor, cancellationToken);

    public Task<List<PcMonitorEntity>> GetAllMonitorsAsync(CancellationToken cancellationToken)=>
        cache.GetOrCreateAsync(ComponentCacheKeys.MonitorsKey, inner.GetAllMonitorsAsync, cancellationToken);

    public Task<PcMonitorEntity?> GetMonitorByIdAsync(int id, CancellationToken cancellationToken)=>
        inner.GetMonitorByIdAsync(id, cancellationToken);

    public Task SaveChangesAsync(CancellationToken cancellationToken)=>
        inner.SaveChangesAsync(cancellationToken);
}
