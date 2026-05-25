using PcBuilder.Entities;

namespace PcBuilder.Repositories.Interfaces;

public interface IPcCaseRepository
{
    public Task<List<PcCaseEntity>> GetAllCasesAsync();
    public Task<PcCaseEntity?> GetCaseByIdAsync(int id);
    public Task AddCaseAsync(PcCaseEntity pcCase);
    public Task DeleteCaseAsync(PcCaseEntity pcCase);
    public Task<bool> BrandExistsAsync(int brandId);
    public Task SaveChangesAsync();
}
