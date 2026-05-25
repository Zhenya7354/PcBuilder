using PcBuilder.Entities;

namespace PcBuilder.Repositories.Interfaces;

public interface IPcCaseRepository
{
    public Task<List<PcCaseEntity>> GetAllCasesAsync();
    public Task<PcCaseEntity?> GetCaseByIdAsync(int id);
    public Task AddCase(PcCaseEntity pcCase);
    public Task DeleteCase(PcCaseEntity pcCase);
    public Task<bool> BrandExistsAsync(int brandId);
    public Task SaveChangesAsync();
}
