using PcBuilder.Entities;

namespace PcBuilder.Repositories.Interfaces;

public interface IPsuRepository
{
    public Task<List<PsuEntity>> GetAllPsusAsync();
    public Task<PsuEntity?> GetPsuByIdAsync(int id);
    public Task AddPsu(PsuEntity psu);
    public Task DeletePsu(PsuEntity psu);
    public Task<bool> BrandExistsAsync(int brandId);
    public Task SaveChangesAsync();
}
