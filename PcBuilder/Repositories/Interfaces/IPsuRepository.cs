using PcBuilder.Entities;

namespace PcBuilder.Repositories.Interfaces;

public interface IPsuRepository
{
    public Task<List<PsuEntity>> GetAllPsusAsync(CancellationToken cancellationToken);
    public Task<PsuEntity?> GetPsuByIdAsync(int id, CancellationToken cancellationToken);
    public Task AddPsuAsync(PsuEntity psu, CancellationToken cancellationToken);
    public Task DeletePsuAsync(PsuEntity psu, CancellationToken cancellationToken);
    public Task<bool> BrandExistsAsync(int brandId, CancellationToken cancellationToken);
    public Task SaveChangesAsync(CancellationToken cancellationToken);
}
