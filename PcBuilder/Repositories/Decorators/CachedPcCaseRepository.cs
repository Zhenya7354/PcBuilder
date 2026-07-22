using PcBuilder.Entities;
using PcBuilder.Models;
using PcBuilder.Repositories.Interfaces;
using PcBuilder.Services.Interfaces;

namespace PcBuilder.Repositories.Decorators;

public class CachedPcCaseRepository(
    PcCaseRepository inner,
    IMemoryListCache cache) : IPcCaseRepository
{
    public Task AddCaseAsync(PcCaseEntity pcCase, CancellationToken cancellationToken)=>
        inner.AddCaseAsync(pcCase, cancellationToken);

    public Task<bool> BrandExistsAsync(int brandId, CancellationToken cancellationToken)=>
        inner.BrandExistsAsync(brandId, cancellationToken);

    public Task DeleteCaseAsync(PcCaseEntity pcCase, CancellationToken cancellationToken)=>
        inner.DeleteCaseAsync(pcCase, cancellationToken);

    public Task<List<PcCaseEntity>> GetAllCasesAsync(CancellationToken cancellationToken)=>
        cache.GetOrCreateAsync(ComponentCacheKeys.PcCasesKey, inner.GetAllCasesAsync, cancellationToken);

    public Task<PcCaseEntity?> GetCaseByIdAsync(int id, CancellationToken cancellationToken)=>
        inner.GetCaseByIdAsync(id, cancellationToken);

    public Task SaveChangesAsync(CancellationToken cancellationToken)=>
        inner.SaveChangesAsync(cancellationToken);
}
