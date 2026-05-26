using PcBuilder.Entities;

namespace PcBuilder.Repositories.Interfaces;

public interface IBrandRepository
{
    public Task<List<BrandEntity>> GetAllBrandsAsync();
    public Task<BrandEntity> GetBrandByIdAsync(int id);
    public Task AddBrandAsync(BrandEntity brand);
    public Task DeleteBrandAsync(BrandEntity brand);
    public Task SaveChangesAsync();
}
