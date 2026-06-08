using PcBuilder.Entities;
using PcBuilder.Models;
using PcBuilder.Repositories;
using PcBuilder.Repositories.Interfaces;
using PcBuilder.Services.Interfaces;

namespace PcBuilder.Services;

public class PsuService(IPsuRepository psuRepository) : IPsuService
{
    private readonly IPsuRepository _psuRepository = psuRepository;

    public async Task<List<PsuEntity>> GetAllPsusAsync(CancellationToken cancellationToken)
    {
        return await _psuRepository.GetAllPsusAsync(cancellationToken);
    }

    public async Task<PsuEntity> GetPsuByIdAsync(int id, CancellationToken cancellationToken)
    {
        var psu = await _psuRepository.GetPsuByIdAsync(id, cancellationToken) ??
            throw new KeyNotFoundException($"PSU with ID {id} not found.");

        return psu;
    }

    public async Task<PsuEntity> AddPsuAsync(PsuCreateRequest dto, CancellationToken cancellationToken)
    {
        await EnsureBrandExistsAsync(dto.BrandId, cancellationToken);

        var psu = new PsuEntity
        {
            Name = dto.Name,
            BrandId = dto.BrandId,
            Wattage = dto.Wattage,
            Efficiency = dto.Efficiency,
            Modularity = dto.Modularity,
            AtxVersion = dto.AtxVersion,
            Has16Pin = dto.Has16Pin,
            EpsConnectors = dto.EpsConnectors,
            SataConnectors = dto.SataConnectors,
            Pcie8PinConnectors = dto.Pcie8PinConnectors,
            FanSizeMm = dto.FanSizeMm,
            LengthMm = dto.LengthMm,
            Currency = dto.Currency,
            Price = dto.Price
        };

        await _psuRepository.AddPsuAsync(psu, cancellationToken);
        await _psuRepository.SaveChangesAsync(cancellationToken);
        return psu;
    }

    public async Task<PsuEntity> UpdatePsuAsync(int id, PsuUpdateRequest dto, CancellationToken cancellationToken)
    {
        var psu = await _psuRepository.GetPsuByIdAsync(id, cancellationToken) ??
            throw new KeyNotFoundException($"PSU with ID {id} not found.");

        await EnsureBrandExistsAsync(dto.BrandId ?? psu.BrandId, cancellationToken);

        if (!string.IsNullOrWhiteSpace(dto.Name)) psu.Name = dto.Name;
        if (dto.Wattage.HasValue) psu.Wattage = dto.Wattage.Value;
        if (dto.Efficiency.HasValue) psu.Efficiency = dto.Efficiency.Value;
        if (dto.Modularity.HasValue) psu.Modularity = dto.Modularity.Value;
        if (!string.IsNullOrWhiteSpace(dto.AtxVersion)) psu.AtxVersion = dto.AtxVersion;
        if (dto.Has16Pin.HasValue) psu.Has16Pin = dto.Has16Pin.Value;
        if (dto.EpsConnectors.HasValue) psu.EpsConnectors = dto.EpsConnectors.Value;
        if (dto.SataConnectors.HasValue) psu.SataConnectors = dto.SataConnectors.Value;
        if (dto.Pcie8PinConnectors.HasValue) psu.Pcie8PinConnectors = dto.Pcie8PinConnectors.Value;
        if (dto.FanSizeMm.HasValue) psu.FanSizeMm = dto.FanSizeMm.Value;
        if (dto.LengthMm.HasValue) psu.LengthMm = dto.LengthMm.Value;
        if(dto.Currency.HasValue) psu.Currency = dto.Currency.Value;
        if (dto.Price.HasValue) psu.Price = dto.Price.Value;

        await _psuRepository.SaveChangesAsync(cancellationToken);
        return psu;
    }

    public async Task DeletePsuAsync(int id, CancellationToken cancellationToken)
    {
        var psu = await _psuRepository.GetPsuByIdAsync(id, cancellationToken) ??
            throw new KeyNotFoundException($"PSU with ID {id} not found.");

        await _psuRepository.DeletePsuAsync(psu, cancellationToken);
        await _psuRepository.SaveChangesAsync(cancellationToken);
    }

    private async Task EnsureBrandExistsAsync(int brandId, CancellationToken cancellationToken)
    {
        if (!await _psuRepository.BrandExistsAsync(brandId, cancellationToken))
        {
            throw new KeyNotFoundException("Brand with the specified ID does not exist.");
        }
    }
}
