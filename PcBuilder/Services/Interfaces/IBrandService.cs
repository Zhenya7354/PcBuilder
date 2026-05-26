using PcBuilder.Entities;
using PcBuilder.Models;

namespace PcBuilder.Services.Interfaces;

public interface IBrandService
{
    public Task<List<BrandEntity>> GetAllBrandsAsync();
    public Task<BrandEntity> GetBrandByIdAsync(int id);
    public Task<BrandEntity> AddBrandAsync(BrandCreate dto);
    public Task<BrandEntity> UpdateBrandAsync(int id, BrandUpdate dto);
    public Task DeleteBrandAsync(int id);
}
