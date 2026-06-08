using PcBuilder.Entities;

namespace PcBuilder.Repositories.Interfaces;

public interface IPcCaseRepository
{
    public Task<List<PcCaseEntity>> GetAllCasesAsync(CancellationToken cancellationToken);
    public Task<PcCaseEntity?> GetCaseByIdAsync(int id, CancellationToken cancellationToken);
    public Task AddCaseAsync(PcCaseEntity pcCase, CancellationToken cancellationToken);
    public Task DeleteCaseAsync(PcCaseEntity pcCase, CancellationToken cancellationToken);
    public Task<bool> BrandExistsAsync(int brandId, CancellationToken cancellationToken);
    public Task SaveChangesAsync(CancellationToken cancellationToken);
}
