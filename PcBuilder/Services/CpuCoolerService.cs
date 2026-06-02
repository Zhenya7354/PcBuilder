using PcBuilder.Models;
using PcBuilder.Entities;
using PcBuilder.Repositories.Interfaces;
using PcBuilder.Services.Interfaces;

namespace PcBuilder.Services;

public class CpuCoolerService(ICpuCoolerRepository cpuCoolerRepository) : ICpuCoolerService
{
    private readonly ICpuCoolerRepository _cpuCoolerRepository = cpuCoolerRepository;

    public async Task<List<CpuCoolerEntity>> GetAllCpuCoolersAsync()
    {
        return await _cpuCoolerRepository.GetAllCpuCoolersAsync();
    }

    public async Task<CpuCoolerEntity> GetCpuCoolerByIdAsync(int id)
    {
        var cpuCooler = await _cpuCoolerRepository.GetCpuCoolerByIdAsync(id) ??
            throw new KeyNotFoundException($"CPU cooler with ID {id} not found.");

        return cpuCooler;
    }

    public async Task<CpuCoolerEntity> AddCpuCoolerAsync(CpuCoolerCreateRequest dto)
    {
        await EnsureBrandExistsAsync(dto.BrandId);

        var cpuCooler = new CpuCoolerEntity
        {
            Name = dto.Name,
            BrandId = dto.BrandId,
            CoolerType = dto.CoolerType,
            SocketsSupported = dto.SocketsSupported,
            RadiatorSizeMm = dto.RadiatorSizeMm,
            FanCount = dto.FanCount,
            FanSizeMm = dto.FanSizeMm,
            MaxTdpWatts = dto.MaxTdpWatts,
            HeightMm = dto.HeightMm,
            HasRgb = dto.HasRgb,
            NoiseLevelDb = dto.NoiseLevelDb,
            PriceUsd = dto.PriceUsd
        };

        await _cpuCoolerRepository.AddCpuCoolerAsync(cpuCooler);
        await _cpuCoolerRepository.SaveChangesAsync();
        return cpuCooler;
    }

    public async Task<CpuCoolerEntity> UpdateCpuCoolerAsync(int id, CpuCoolerUpdateRequest dto)
    {
        var cpuCooler = await _cpuCoolerRepository.GetCpuCoolerByIdAsync(id) ??
            throw new KeyNotFoundException($"CPU cooler with ID {id} not found.");
        await EnsureBrandExistsAsync(dto.BrandId ?? cpuCooler.BrandId);

        if (!string.IsNullOrWhiteSpace(dto.Name)) cpuCooler.Name = dto.Name;
        if (dto.CoolerType.HasValue) cpuCooler.CoolerType = dto.CoolerType.Value;
        if (dto.SocketsSupported is { Count: > 0 }) cpuCooler.SocketsSupported = dto.SocketsSupported;
        if (dto.RadiatorSizeMm.HasValue) cpuCooler.RadiatorSizeMm = dto.RadiatorSizeMm.Value;
        if (dto.FanCount.HasValue) cpuCooler.FanCount = dto.FanCount.Value;
        if (dto.FanSizeMm.HasValue) cpuCooler.FanSizeMm = dto.FanSizeMm.Value;
        if (dto.MaxTdpWatts.HasValue) cpuCooler.MaxTdpWatts = dto.MaxTdpWatts.Value;
        if (dto.HeightMm.HasValue) cpuCooler.HeightMm = dto.HeightMm.Value;
        if (dto.HasRgb.HasValue) cpuCooler.HasRgb = dto.HasRgb.Value;
        if (dto.NoiseLevelDb.HasValue) cpuCooler.NoiseLevelDb = dto.NoiseLevelDb.Value;
        if (dto.PriceUsd.HasValue) cpuCooler.PriceUsd = dto.PriceUsd.Value;

        await _cpuCoolerRepository.SaveChangesAsync();
        return cpuCooler;
    }

    public async Task DeleteCpuCoolerAsync(int id)
    {
        var cpuCooler = await _cpuCoolerRepository.GetCpuCoolerByIdAsync(id) ??
            throw new KeyNotFoundException($"CPU cooler with ID {id} not found.");

        await _cpuCoolerRepository.DeleteCpuCoolerAsync(cpuCooler);
        await _cpuCoolerRepository.SaveChangesAsync();
    }

    private async Task EnsureBrandExistsAsync(int brandId)
    {
        if (!await _cpuCoolerRepository.BrandExistsAsync(brandId))
        {
            throw new KeyNotFoundException("Brand with the specified ID does not exist.");
        }
    }
}
