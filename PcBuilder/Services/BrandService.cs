using PcBuilder.Models;
using PcBuilder.Entities;
using PcBuilder.Repositories.Interfaces;
using PcBuilder.Services.Interfaces;

namespace PcBuilder.Services;

public class BrandService(IBrandRepository brandRepository) : IBrandService
{
    private readonly IBrandRepository _brandRepository = brandRepository;

    public async Task<List<BrandEntity>> GetAllBrandsAsync(CancellationToken cancellationToken)
    {
        return await _brandRepository.GetAllBrandsAsync(cancellationToken);
    }

    public async Task<BrandEntity> GetBrandByIdAsync(int id, CancellationToken cancellationToken)
    {
        var brand = await _brandRepository.GetBrandByIdAsync(id, cancellationToken)
            ?? throw new KeyNotFoundException($"Brand with ID {id} not found.");

        return brand;
    }

    public async Task<BrandEntity> AddBrandAsync(BrandCreateRequest dto, CancellationToken cancellationToken)
    {
        if (dto is null) 
            throw new ArgumentNullException("Brand data is required");
        var brand = new BrandEntity { Name = dto.Name };
        await _brandRepository.AddBrandAsync(brand, cancellationToken);
        await _brandRepository.SaveChangesAsync(cancellationToken);
        return brand;
    }

    public async Task<BrandEntity> UpdateBrandAsync(int id, BrandUpdateRequest dto, CancellationToken cancellationToken)
    {
        if (dto is null)
            throw new ArgumentNullException("Brand data is required");
        var brand = await _brandRepository.GetBrandByIdAsync(id, cancellationToken) ??
            throw new KeyNotFoundException($"Brand with ID {id} not found.");

        if (!string.IsNullOrWhiteSpace(dto.Name)) brand.Name = dto.Name;

        await _brandRepository.SaveChangesAsync(cancellationToken);
        return brand;
    }

    public async Task DeleteBrandAsync(int id, CancellationToken cancellationToken)
    {
        var brand = await _brandRepository.GetBrandByIdAsync(id, cancellationToken) ??
           throw new KeyNotFoundException($"Brand with ID {id} not found.");

        await _brandRepository.DeleteBrandAsync(brand, cancellationToken);
        await _brandRepository.SaveChangesAsync(cancellationToken);
    }
}
