using PcBuilder.Entities;
using PcBuilder.Models;
using PcBuilder.Repositories;
using PcBuilder.Repositories.Interfaces;
using PcBuilder.Services.Interfaces;

namespace PcBuilder.Services;

public class RamService(IRamRepository ramRepository) : IRamService
{
    private readonly IRamRepository _ramRepository = ramRepository;

    public async Task<List<RamEntity>> GetAllRamAsync()
    {
        return await _ramRepository.GetAllRamAsync();
    }

    public async Task<RamEntity> GetRamByIdAsync(int id)
    {
        var ram = await _ramRepository.GetRamByIdAsync(id) ??
            throw new KeyNotFoundException($"RAM with ID {id} not found.");

        return ram;
    }

    public async Task<RamEntity> AddRamAsync(RamCreate dto)
    {
        await EnsureBrandExistsAsync(dto.BrandId);

        var ram = new RamEntity
        {
            Name = dto.Name,
            BrandId = dto.BrandId,
            MemoryType = dto.MemoryType,
            CapacityGb = dto.CapacityGb,
            KitCount = dto.KitCount,
            SpeedMhz = dto.SpeedMhz,
            CasLatency = dto.CasLatency,
            Voltage = dto.Voltage,
            HasRgb = dto.HasRgb,
            HasEcc = dto.HasEcc,
            HeightMm = dto.HeightMm,
            PriceUsd = dto.PriceUsd
        };

        await _ramRepository.AddRamAsync(ram);
        await _ramRepository.SaveChangesAsync();
        return ram;
    }

    public async Task<RamEntity> UpdateRamAsync(int id, RamUpdate dto)
    {
        var ram = await _ramRepository.GetRamByIdAsync(id) ??
            throw new KeyNotFoundException($"RAM with ID {id} not found.");

        await EnsureBrandExistsAsync(dto.BrandId ?? ram.BrandId);

        if (!string.IsNullOrWhiteSpace(dto.Name)) ram.Name = dto.Name;
        if (dto.MemoryType.HasValue) ram.MemoryType = dto.MemoryType.Value;
        if (dto.CapacityGb.HasValue) ram.CapacityGb = dto.CapacityGb.Value;
        if (dto.KitCount.HasValue) ram.KitCount = dto.KitCount.Value;
        if (dto.SpeedMhz.HasValue) ram.SpeedMhz = dto.SpeedMhz.Value;
        if (dto.CasLatency.HasValue) ram.CasLatency = dto.CasLatency.Value;
        if (dto.Voltage.HasValue) ram.Voltage = dto.Voltage.Value;
        if (dto.HasRgb.HasValue) ram.HasRgb = dto.HasRgb.Value;
        if (dto.HasEcc.HasValue) ram.HasEcc = dto.HasEcc.Value;
        if (dto.HeightMm.HasValue) ram.HeightMm = dto.HeightMm.Value;
        if (dto.PriceUsd.HasValue) ram.PriceUsd = dto.PriceUsd.Value;

        await _ramRepository.SaveChangesAsync();
        return ram;
    }

    public async Task DeleteRamAsync(int id)
    {
        var ram = await _ramRepository.GetRamByIdAsync(id) ??
            throw new KeyNotFoundException($"RAM with ID {id} not found.");

        await _ramRepository.DeleteRamAsync(ram);
        await _ramRepository.SaveChangesAsync();
    }

    private async Task EnsureBrandExistsAsync(int brandId)
    {
        if (!await _ramRepository.BrandExistsAsync(brandId))
        {
            throw new KeyNotFoundException("Brand with the specified ID does not exist.");
        }
    }
}
