using PcBuilder.Models;
using PcBuilder.Entities;
using PcBuilder.Repositories.Interfaces;
using PcBuilder.Services.Interfaces;

namespace PcBuilder.Services;

public class BrandService(IBrandRepository brandRepository) : IBrandService
{
    private readonly IBrandRepository _brandRepository = brandRepository;

    public async Task<List<BrandEntity>> GetAllBrandsAsync()
    {
        return await _brandRepository.GetAllBrandsAsync();
    }

    public async Task<BrandEntity> GetBrandByIdAsync(int id)
    {
        var brand = await _brandRepository.GetBrandByIdAsync(id)
            ?? throw new KeyNotFoundException($"Brand with ID {id} not found.");

        return brand;
    }

    public async Task<BrandEntity> AddBrandAsync(BrandCreateRequest dto)
    {
        var brand = new BrandEntity { Name = dto.Name };
        await _brandRepository.AddBrandAsync(brand);
        await _brandRepository.SaveChangesAsync();
        return brand;
    }

    public async Task<BrandEntity> UpdateBrandAsync(int id, BrandUpdateRequest dto)
    {
        var brand = await _brandRepository.GetBrandByIdAsync(id) ??
            throw new KeyNotFoundException($"Brand with ID {id} not found.");

        if (!string.IsNullOrWhiteSpace(dto.Name)) brand.Name = dto.Name;

        await _brandRepository.SaveChangesAsync();
        return brand;
    }

    public async Task DeleteBrandAsync(int id)
    {
        var brand = await _brandRepository.GetBrandByIdAsync(id) ??
           throw new KeyNotFoundException($"Brand with ID {id} not found.");

        await _brandRepository.DeleteBrandAsync(brand);
        await _brandRepository.SaveChangesAsync();
    }
}
