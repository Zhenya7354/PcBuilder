using PcBuilder.Entities;
using PcBuilder.Models;
using PcBuilder.Repositories;
using PcBuilder.Repositories.Interfaces;
using PcBuilder.Services.Interfaces;

namespace PcBuilder.Services;

public class RamService(IRamRepository ramRepository) : IRamService
{
    private readonly IRamRepository _ramRepository = ramRepository;

    public async Task<List<RamEntity>> GetAllRamAsync(CancellationToken cancellationToken)
    {
        return await _ramRepository.GetAllRamAsync(cancellationToken);
    }

    public async Task<RamEntity> GetRamByIdAsync(int id, CancellationToken cancellationToken)
    {
        var ram = await _ramRepository.GetRamByIdAsync(id, cancellationToken) ??
            throw new KeyNotFoundException($"RAM with ID {id} not found.");

        return ram;
    }

    public async Task<RamEntity> AddRamAsync(RamCreateRequest dto, CancellationToken cancellationToken)
    {
        if (dto is null)
            throw new ArgumentNullException("RAM data is required.");

        await EnsureBrandExistsAsync(dto.BrandId, cancellationToken);

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
            ColorScheme = dto.ColorScheme,
            HasEcc = dto.HasEcc,
            HeightMm = dto.HeightMm,
            Currency = dto.Currency,
            Price = dto.Price
        };

        await _ramRepository.AddRamAsync(ram, cancellationToken);
        await _ramRepository.SaveChangesAsync(cancellationToken);
        return ram;
    }

    public async Task<RamEntity> UpdateRamAsync(int id, RamUpdateRequest dto, CancellationToken cancellationToken)
    {
        if (dto is null)
            throw new ArgumentNullException("RAM data is required.");

        var ram = await _ramRepository.GetRamByIdAsync(id, cancellationToken) ??
            throw new KeyNotFoundException($"RAM with ID {id} not found.");

        await EnsureBrandExistsAsync(dto.BrandId ?? ram.BrandId, cancellationToken);

        if (!string.IsNullOrWhiteSpace(dto.Name)) ram.Name = dto.Name;
        if (dto.MemoryType.HasValue) ram.MemoryType = dto.MemoryType.Value;
        if (dto.CapacityGb.HasValue) ram.CapacityGb = dto.CapacityGb.Value;
        if (dto.KitCount.HasValue) ram.KitCount = dto.KitCount.Value;
        if (dto.SpeedMhz.HasValue) ram.SpeedMhz = dto.SpeedMhz.Value;
        if (dto.CasLatency.HasValue) ram.CasLatency = dto.CasLatency.Value;
        if (dto.Voltage.HasValue) ram.Voltage = dto.Voltage.Value;
        if (dto.ColorScheme.HasValue) ram.ColorScheme = dto.ColorScheme.Value;
        if (dto.HasEcc.HasValue) ram.HasEcc = dto.HasEcc.Value;
        if (dto.HeightMm.HasValue) ram.HeightMm = dto.HeightMm.Value;
        if(dto.Currency.HasValue) ram.Currency = dto.Currency.Value;
        if (dto.Price.HasValue) ram.Price = dto.Price.Value;

        await _ramRepository.SaveChangesAsync(cancellationToken);
        return ram;
    }

    public async Task DeleteRamAsync(int id, CancellationToken cancellationToken)
    {
        var ram = await _ramRepository.GetRamByIdAsync(id, cancellationToken) ??
            throw new KeyNotFoundException($"RAM with ID {id} not found.");

        await _ramRepository.DeleteRamAsync(ram, cancellationToken);
        await _ramRepository.SaveChangesAsync(cancellationToken);
    }

    private async Task EnsureBrandExistsAsync(int brandId, CancellationToken cancellationToken)
    {
        if (!await _ramRepository.BrandExistsAsync(brandId, cancellationToken))
        {
            throw new KeyNotFoundException("Brand with the specified ID does not exist.");
        }
    }
}
