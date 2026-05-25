using PcBuilder.Entities;

namespace PcBuilder.Repositories.Interfaces;

public interface IBrandRepository
{
    public Task<List<BrandEntity>> GetAllBrandsAsync();
    public Task<BrandEntity?> GetBrandByIdAsync(int id);
    public Task AddBrand(BrandEntity brand);
    public Task DeleteBrand(BrandEntity brand);
    public Task SaveChangesAsync();
}
