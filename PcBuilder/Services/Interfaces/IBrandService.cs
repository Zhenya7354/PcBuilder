using PcBuilder.Entities;
using PcBuilder.Models;

namespace PcBuilder.Services.Interfaces;

public interface IBrandService
{
    public Task<List<BrandEntity>> GetAllBrandsAsync();
    public Task<BrandEntity> GetBrandByIdAsync(int id);
    public Task<BrandEntity> AddBrandAsync(BrandCreateRequest dto);
    public Task<BrandEntity> UpdateBrandAsync(int id, BrandUpdateRequest dto);
    public Task DeleteBrandAsync(int id);
}
