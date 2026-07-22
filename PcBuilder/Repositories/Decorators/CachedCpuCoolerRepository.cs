using PcBuilder.Entities;
using PcBuilder.Models;
using PcBuilder.Repositories.Interfaces;
using PcBuilder.Services.Interfaces;

namespace PcBuilder.Repositories.Decorators;

public class CachedCpuCoolerRepository(
    CpuCoolerRepository inner,
    IMemoryListCache cache) : ICpuCoolerRepository
{
    public Task AddCpuCoolerAsync(CpuCoolerEntity cpuCooler, CancellationToken cancellationToken) =>
        inner.AddCpuCoolerAsync(cpuCooler, cancellationToken);

    public Task<bool> BrandExistsAsync(int brandId, CancellationToken cancellationToken) =>
        inner.BrandExistsAsync(brandId, cancellationToken);

    public Task DeleteCpuCoolerAsync(CpuCoolerEntity cpuCooler, CancellationToken cancellationToken) =>
        inner.DeleteCpuCoolerAsync(cpuCooler, cancellationToken);

    public Task<List<CpuCoolerEntity>> GetAllCpuCoolersAsync(CancellationToken cancellationToken) =>
        cache.GetOrCreateAsync(ComponentCacheKeys.CpuCoolersKey, inner.GetAllCpuCoolersAsync, cancellationToken);

    public Task<CpuCoolerEntity?> GetCpuCoolerByIdAsync(int id, CancellationToken cancellationToken) =>
        inner.GetCpuCoolerByIdAsync(id,cancellationToken);

    public Task SaveChangesAsync(CancellationToken cancellationToken)=>
        inner.SaveChangesAsync(cancellationToken);
}
