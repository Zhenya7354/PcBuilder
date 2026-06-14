using PcBuilder.Models;
using PcBuilder.Entities;
using PcBuilder.Repositories.Interfaces;
using PcBuilder.Services.Interfaces;

namespace PcBuilder.Services;

public class CpuCoolerService(ICpuCoolerRepository cpuCoolerRepository) : ICpuCoolerService
{
    private readonly ICpuCoolerRepository _cpuCoolerRepository = cpuCoolerRepository;

    public async Task<List<CpuCoolerEntity>> GetAllCpuCoolersAsync(CancellationToken cancellationToken)
    {
        return await _cpuCoolerRepository.GetAllCpuCoolersAsync(cancellationToken);
    }

    public async Task<CpuCoolerEntity> GetCpuCoolerByIdAsync(int id, CancellationToken cancellationToken)
    {
        var cpuCooler = await _cpuCoolerRepository.GetCpuCoolerByIdAsync(id, cancellationToken) ??
            throw new KeyNotFoundException($"CPU cooler with ID {id} not found.");

        return cpuCooler;
    }

    public async Task<CpuCoolerEntity> AddCpuCoolerAsync(CpuCoolerCreateRequest dto, CancellationToken cancellationToken)
    {
        if (dto is null)
            throw new ArgumentNullException("Cpu cooler data is required");
        await EnsureBrandExistsAsync(dto.BrandId, cancellationToken);

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
            ColorScheme = dto.ColorScheme,
            NoiseLevelDb = dto.NoiseLevelDb,
            Currency = dto.Currency,
            Price = dto.Price
        };

        await _cpuCoolerRepository.AddCpuCoolerAsync(cpuCooler, cancellationToken);
        await _cpuCoolerRepository.SaveChangesAsync(cancellationToken);
        return cpuCooler;
    }

    public async Task<CpuCoolerEntity> UpdateCpuCoolerAsync(int id, CpuCoolerUpdateRequest dto, CancellationToken cancellationToken)
    {
        if (dto is null)
            throw new ArgumentNullException("Cpu cooler data is required");

        var cpuCooler = await _cpuCoolerRepository.GetCpuCoolerByIdAsync(id, cancellationToken) ??
            throw new KeyNotFoundException($"CPU cooler with ID {id} not found.");
        await EnsureBrandExistsAsync(dto.BrandId ?? cpuCooler.BrandId, cancellationToken);

        if (!string.IsNullOrWhiteSpace(dto.Name)) cpuCooler.Name = dto.Name;
        if (dto.CoolerType.HasValue) cpuCooler.CoolerType = dto.CoolerType.Value;
        if (dto.SocketsSupported is { Count: > 0 }) cpuCooler.SocketsSupported = dto.SocketsSupported;
        if (dto.RadiatorSizeMm.HasValue) cpuCooler.RadiatorSizeMm = dto.RadiatorSizeMm.Value;
        if (dto.FanCount.HasValue) cpuCooler.FanCount = dto.FanCount.Value;
        if (dto.FanSizeMm.HasValue) cpuCooler.FanSizeMm = dto.FanSizeMm.Value;
        if (dto.MaxTdpWatts.HasValue) cpuCooler.MaxTdpWatts = dto.MaxTdpWatts.Value;
        if (dto.HeightMm.HasValue) cpuCooler.HeightMm = dto.HeightMm.Value;
        if (dto.ColorScheme.HasValue) cpuCooler.ColorScheme = dto.ColorScheme.Value;
        if (dto.NoiseLevelDb.HasValue) cpuCooler.NoiseLevelDb = dto.NoiseLevelDb.Value;
        if (dto.Currency.HasValue) cpuCooler.Currency = dto.Currency.Value;
        if (dto.Price.HasValue) cpuCooler.Price = dto.Price.Value;

        await _cpuCoolerRepository.SaveChangesAsync(cancellationToken);
        return cpuCooler;
    }

    public async Task DeleteCpuCoolerAsync(int id, CancellationToken cancellationToken)
    {
        var cpuCooler = await _cpuCoolerRepository.GetCpuCoolerByIdAsync(id, cancellationToken) ??
            throw new KeyNotFoundException($"CPU cooler with ID {id} not found.");

        await _cpuCoolerRepository.DeleteCpuCoolerAsync(cpuCooler, cancellationToken);
        await _cpuCoolerRepository.SaveChangesAsync(cancellationToken);
    }

    private async Task EnsureBrandExistsAsync(int brandId, CancellationToken cancellationToken)
    {
        if (!await _cpuCoolerRepository.BrandExistsAsync(brandId, cancellationToken))
        {
            throw new KeyNotFoundException("Brand with the specified ID does not exist.");
        }
    }
}
