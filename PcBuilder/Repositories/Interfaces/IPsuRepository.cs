using PcBuilder.Entities;

namespace PcBuilder.Repositories.Interfaces;

public interface IPsuRepository
{
    public Task<List<PsuEntity>> GetAllPsusAsync();
    public Task<PsuEntity?> GetPsuByIdAsync(int id);
    public Task AddPsuAsync(PsuEntity psu);
    public Task DeletePsuAsync(PsuEntity psu);
    public Task<bool> BrandExistsAsync(int brandId);
    public Task SaveChangesAsync();
}
