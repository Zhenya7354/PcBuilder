using PcBuilder.Entities;
using PcBuilder.Models;

namespace PcBuilder.Services.Interfaces;

public interface IBrandService
{
    public Task<List<BrandEntity>> GetAllBrandsAsync(CancellationToken cancellationToken);
    public Task<BrandEntity> GetBrandByIdAsync(int id, CancellationToken cancellationToken);
    public Task<BrandEntity> AddBrandAsync(BrandCreateRequest dto, CancellationToken cancellationToken);
    public Task<BrandEntity> UpdateBrandAsync(int id, BrandUpdateRequest dto, CancellationToken cancellationToken);
    public Task DeleteBrandAsync(int id, CancellationToken cancellationToken);
}
