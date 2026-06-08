using PcBuilder.Entities;

namespace PcBuilder.Repositories.Interfaces;

public interface IBrandRepository
{
    public Task<List<BrandEntity>> GetAllBrandsAsync(CancellationToken cancellationToken);
    public Task<BrandEntity?> GetBrandByIdAsync(int id, CancellationToken cancellationToken);
    public Task AddBrandAsync(BrandEntity brand, CancellationToken cancellationToken);
    public Task DeleteBrandAsync(BrandEntity brand, CancellationToken cancellationToken);
    public Task SaveChangesAsync(CancellationToken cancellationToken);
}
